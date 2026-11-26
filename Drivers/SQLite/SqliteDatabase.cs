using System.Data;
using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;
using MyRestAPI.Drivers.SQLite.Tables;
using Object = MyRestAPI.Common.DB.Entity.Object;

namespace MyRestAPI.Drivers.SQLite;

/// <summary>
/// Контекст базы данных SQLite. Хранит строку подключения и единый Mutex для всех операций.
/// </summary>
public class SqliteDatabase : IDatabase
{
    private readonly string _connectionString;
    private readonly Mutex _mutex = new(false);
    private int _released;
    private int _activeOperations;

    public IClient Clients { get; }
    public IManager Managers { get; }
    public IInsurance_contract Contracts { get; }
    public IObject Objects { get; }
    public IType_of_insurance InsuranceTypes { get; }
    public IType_of_object ObjectTypes { get; }
    public IDBUnits Units => throw new NotImplementedException();

    public SqliteDatabase(string connectionString = "Data Source=insurance.db")
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Строка подключения SQLite не может быть пустой.", nameof(connectionString));

        _connectionString = connectionString;

        Clients = new Client_Table(new SqliteExecutor<Client>(_connectionString, _mutex, BeginOperation, EndOperation));
        Managers = new Manager_Table(new SqliteExecutor<Manager>(_connectionString, _mutex, BeginOperation, EndOperation));
        Contracts = new Insurance_contract_Table(new SqliteExecutor<Insurance_contract>(_connectionString, _mutex, BeginOperation, EndOperation));
        Objects = new Object_Table(new SqliteExecutor<Object>(_connectionString, _mutex, BeginOperation, EndOperation));
        InsuranceTypes = new Type_of_insurance_Table(new SqliteExecutor<Type_of_insurance>(_connectionString, _mutex, BeginOperation, EndOperation));
        ObjectTypes = new Type_of_object_Table(new SqliteExecutor<Type_of_object>(_connectionString, _mutex, BeginOperation, EndOperation));
    }

    public bool TestConnection()
    {
        BeginOperation();
        try
        {
            _mutex.WaitOne();
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                return connection.State == ConnectionState.Open;
            }
            catch
            {
                return false;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
        finally
        {
            EndOperation();
        }
    }

    public void Initialize()
    {
        // Порядок создания учитывает зависимости внешних ключей
        string[] createScripts =
        [
            Type_of_object_Table.CreateSql,
            Type_of_insurance_Table.CreateSql,
            Manager_Table.CreateSql,
            Client_Table.CreateSql,
            Object_Table.CreateSql,
            Insurance_contract_Table.CreateSql
        ];

        BeginOperation();
        try
        {
            _mutex.WaitOne();
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                using (var pragma = connection.CreateCommand())
                {
                    pragma.CommandText = "PRAGMA foreign_keys = ON;";
                    pragma.ExecuteNonQuery();
                }

                foreach (var sql in createScripts)
                {
                    using var command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
        finally
        {
            EndOperation();
        }
    }

    internal void BeginOperation()
    {
        Interlocked.Increment(ref _activeOperations);
        if (Volatile.Read(ref _released) != 0)
        {
            Interlocked.Decrement(ref _activeOperations);
            throw new ObjectDisposedException(nameof(SqliteDatabase));
        }
    }

    internal void EndOperation()
    {
        Interlocked.Decrement(ref _activeOperations);
    }

    public void Release()
    {
        if (Interlocked.Exchange(ref _released, 1) != 0)
            return;

        SpinWait.SpinUntil(() => Volatile.Read(ref _activeOperations) == 0);
        _mutex.Dispose();
    }

    public void Dispose()
    {
        Release();
    }
}

