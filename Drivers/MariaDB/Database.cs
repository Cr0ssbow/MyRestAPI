using MySqlConnector;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Database : IDataBase
{
    private readonly string _connectionString;

    public IClient Clients { get; }
    public IManager Managers { get; }
    public IInsurance_contract Contracts { get; }
    public IObject Objects { get; }
    public IType_of_insurance InsuranceTypes { get; }
    public IType_of_object ObjectTypes { get; }
    public IDBUnits Units => throw new NotImplementedException();

    public Database(string connectionString = "Server=localhost;Database=insurance_company;User ID=root;Password=;")
    {
        _connectionString = connectionString;

        Clients = new Client_Table(connectionString);
        Managers = new Manager_Table(connectionString);
        Contracts = new Insurance_contract_Table(connectionString);
        Objects = new Object_Table(connectionString);
        InsuranceTypes = new Type_of_insurance_Table(connectionString);
        ObjectTypes = new Type_of_object_Table(connectionString);
    }

    public bool TestConnection()
    {
        try
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Initialize()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        // Инициализация таблиц через их отдельные классы (в порядке зависимостей)
        Type_of_object_Table.EnsureTableCreated(connection);
        Type_of_insurance_Table.EnsureTableCreated(connection);
        Manager_Table.EnsureTableCreated(connection);
        Client_Table.EnsureTableCreated(connection);
        Object_Table.EnsureTableCreated(connection);
        Insurance_contract_Table.EnsureTableCreated(connection);
    }
}
