using System.Data;
using System.Data.Common;

namespace MyRestAPI.Common.DB;

public class DbConnectionWrapper<TConnection> : IDisposable where TConnection : DbConnection, new()
{
    private readonly TConnection _connection;
    private readonly object _lock = new();
    private readonly Action<TConnection>? _afterOpen;
    private bool _disposed;

    public DbConnectionWrapper(string connectionString, Action<TConnection>? afterOpen = null)
    {
        _afterOpen = afterOpen;
        _connection = new TConnection
        {
            ConnectionString = connectionString
        };
    }

    public DbConnectionWrapper(TConnection connection, Action<TConnection>? afterOpen = null)
    {
        _afterOpen = afterOpen;
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public TResult Execute<TResult>(Func<TConnection, TResult> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        lock (_lock)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            EnsureOpen();
            return operation(_connection);
        }
    }

    public void Execute(Action<TConnection> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        Execute(conn =>
        {
            operation(conn);
            return true;
        });
    }

    public TResult ExecuteTransaction<TResult>(Func<TConnection, DbTransaction, TResult> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        lock (_lock)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            EnsureOpen();
            using var transaction = _connection.BeginTransaction();
            try
            {
                var result = operation(_connection, transaction);
                transaction.Commit();
                return result;
            }
            catch
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {
                    // Игнорируем исключение отката (например, при обрыве соединения),
                    // чтобы не затереть исходную ошибку операции.
                }
                throw;
            }
        }
    }

    public void ExecuteTransaction(Action<TConnection, DbTransaction> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ExecuteTransaction((conn, trans) =>
        {
            operation(conn, trans);
            return true;
        });
    }

    private void EnsureOpen()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
            _afterOpen?.Invoke(_connection);
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            if (!_disposed)
            {
                _connection.Dispose();
                _disposed = true;
            }
        }
    }
}
