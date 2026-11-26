using System.Data;
using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;
using MyRestAPI.Drivers.MariaDB.Tables;
using Object = MyRestAPI.Common.DB.Entity.Object;

namespace MyRestAPI.Drivers.MariaDB;

/// <summary>
/// Контекст базы данных MariaDB. Хранит эталонное соединение для клонирования.
/// </summary>
public class MariaDbDatabase : IDatabase
{
    private readonly MySqlConnection _prototypeConnection;
    private int _released;
    private int _activeOperations;

    public IClient Clients { get; }
    public IManager Managers { get; }
    public IInsurance_contract Contracts { get; }
    public IObject Objects { get; }
    public IType_of_insurance InsuranceTypes { get; }
    public IType_of_object ObjectTypes { get; }
    public IDBUnits Units => throw new NotImplementedException();

    public MariaDbDatabase(string connectionString = "Server=localhost;Database=insurance_company;User ID=root;Password=;")
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Строка подключения MariaDB не может быть пустой.", nameof(connectionString));

        _prototypeConnection = new MySqlConnection(connectionString);

        Clients = new Client_Table(new MariaDbExecutor<Client>(_prototypeConnection, BeginOperation, EndOperation));
        Managers = new Manager_Table(new MariaDbExecutor<Manager>(_prototypeConnection, BeginOperation, EndOperation));
        Contracts = new Insurance_contract_Table(new MariaDbExecutor<Insurance_contract>(_prototypeConnection, BeginOperation, EndOperation));
        Objects = new Object_Table(new MariaDbExecutor<Object>(_prototypeConnection, BeginOperation, EndOperation));
        InsuranceTypes = new Type_of_insurance_Table(new MariaDbExecutor<Type_of_insurance>(_prototypeConnection, BeginOperation, EndOperation));
        ObjectTypes = new Type_of_object_Table(new MariaDbExecutor<Type_of_object>(_prototypeConnection, BeginOperation, EndOperation));
    }

    public bool TestConnection()
    {
        BeginOperation();
        try
        {
            using var connection = _prototypeConnection.Clone();
            connection.Open();
            return connection.State == ConnectionState.Open;
        }
        catch
        {
            return false;
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
            using var connection = _prototypeConnection.Clone();
            connection.Open();

            foreach (var sql in createScripts)
            {
                using var command = connection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
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
            throw new ObjectDisposedException(nameof(MariaDbDatabase));
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
        _prototypeConnection.Dispose();
    }

    public void Dispose()
    {
        Release();
    }
}

