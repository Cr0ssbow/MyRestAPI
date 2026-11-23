using System.Data;
using System.Data.Common;
using System.Threading.Channels;

namespace MyRestAPI.Common.DB;

public class DbConnectionWrapper<TConnection> : IDisposable where TConnection : DbConnection, new()
{
    private readonly string _connectionString;
    private readonly Action<TConnection>? _afterOpen;
    private readonly TConnection _writeConnection;
    private readonly Mutex _writeMutex = new(false);
    private readonly Channel<IWriteTask> _writeChannel;
    private readonly Thread _workerThread;
    private readonly object _releaseLock = new();
    private bool _released;

    private interface IWriteTask
    {
        void Execute(TConnection connection);
        void SetException(Exception ex);
    }

    private sealed class WriteTask<TResult> : IWriteTask
    {
        private readonly Func<TConnection, TResult> _action;
        private readonly TaskCompletionSource<TResult> _tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public WriteTask(Func<TConnection, TResult> action)
        {
            _action = action;
        }

        public Task<TResult> Task => _tcs.Task;

        public void Execute(TConnection connection)
        {
            try
            {
                var result = _action(connection);
                _tcs.TrySetResult(result);
            }
            catch (Exception ex)
            {
                _tcs.TrySetException(ex);
            }
        }

        public void SetException(Exception ex)
        {
            _tcs.TrySetException(ex);
        }
    }

    public DbConnectionWrapper(string connectionString, Action<TConnection>? afterOpen = null)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _afterOpen = afterOpen;

        _writeConnection = new TConnection
        {
            ConnectionString = _connectionString
        };

        _writeChannel = Channel.CreateUnbounded<IWriteTask>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });

        _workerThread = new Thread(ProcessQueue)
        {
            IsBackground = true,
            Name = "DbWriteWorker"
        };
        _workerThread.Start();
    }

    private void ProcessQueue()
    {
        try
        {
            while (_writeChannel.Reader.WaitToReadAsync().AsTask().GetAwaiter().GetResult())
            {
                while (_writeChannel.Reader.TryRead(out var task))
                {
                    _writeMutex.WaitOne();
                    try
                    {
                        EnsureWriteConnectionOpen();
                        task.Execute(_writeConnection);
                    }
                    catch (Exception ex)
                    {
                        task.SetException(ex);
                    }
                    finally
                    {
                        _writeMutex.ReleaseMutex();
                    }
                }
            }
        }
        catch (Exception)
        {
            // Остановка рабочего потока при закрытии канала
        }
    }

    private void EnsureWriteConnectionOpen()
    {
        if (_writeConnection.State != ConnectionState.Open)
        {
            if (_writeConnection.State != ConnectionState.Closed)
            {
                try { _writeConnection.Close(); } catch { }
            }
            _writeConnection.Open();
            _afterOpen?.Invoke(_writeConnection);
        }
    }

    public Task<TResult> EnqueueWriteAsync<TResult>(Func<TConnection, TResult> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        if (Thread.CurrentThread == _workerThread)
        {
            throw new InvalidOperationException("Вложенная запись из задания очереди запрещена (риск взаимной блокировки).");
        }

        var task = new WriteTask<TResult>(operation);
        if (_released || !_writeChannel.Writer.TryWrite(task))
        {
            task.SetException(new ObjectDisposedException(GetType().FullName, "Обёртка базы данных остановлена (Release)."));
        }
        return task.Task;
    }

    public TResult ExecuteWrite<TResult>(Func<TConnection, TResult> operation)
    {
        return EnqueueWriteAsync(operation).GetAwaiter().GetResult();
    }

    public void ExecuteWrite(Action<TConnection> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ExecuteWrite(conn =>
        {
            operation(conn);
            return true;
        });
    }

    public TResult ExecuteWriteTransaction<TResult>(Func<TConnection, DbTransaction, TResult> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        return ExecuteWrite(conn =>
        {
            using var transaction = conn.BeginTransaction();
            try
            {
                var result = operation(conn, transaction);
                transaction.Commit();
                return result;
            }
            catch
            {
                try { transaction.Rollback(); } catch { }
                throw;
            }
        });
    }

    public void ExecuteWriteTransaction(Action<TConnection, DbTransaction> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ExecuteWriteTransaction((conn, trans) =>
        {
            operation(conn, trans);
            return true;
        });
    }

    public TResult ExecuteRead<TResult>(Func<TConnection, TResult> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        if (_released)
        {
            throw new ObjectDisposedException(GetType().FullName, "Обёртка базы данных остановлена (Release).");
        }

        using var readConnection = new TConnection
        {
            ConnectionString = _connectionString
        };
        readConnection.Open();
        _afterOpen?.Invoke(readConnection);

        return operation(readConnection);
    }

    public void ExecuteRead(Action<TConnection> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ExecuteRead(conn =>
        {
            operation(conn);
            return true;
        });
    }

    public TResult Execute<TResult>(Func<TConnection, TResult> operation) => ExecuteRead(operation);

    public void Execute(Action<TConnection> operation) => ExecuteRead(operation);

    public TResult ExecuteTransaction<TResult>(Func<TConnection, DbTransaction, TResult> operation) =>
        ExecuteWriteTransaction(operation);

    public void ExecuteTransaction(Action<TConnection, DbTransaction> operation) =>
        ExecuteWriteTransaction(operation);

    public void Release()
    {
        lock (_releaseLock)
        {
            if (_released) return;
            _released = true;

            // 1. Закрыть приём новых записей
            _writeChannel.Writer.TryComplete();

            // 2. Дождаться, пока очередь опустеет и рабочий завершится
            if (_workerThread.IsAlive)
            {
                _workerThread.Join();
            }

            // 3. Закрыть подключение записи
            try
            {
                _writeConnection.Dispose();
            }
            catch { }

            // 4. Освободить мьютекс
            try
            {
                _writeMutex.Dispose();
            }
            catch { }
        }
    }

    public void Dispose()
    {
        Release();
    }
}
