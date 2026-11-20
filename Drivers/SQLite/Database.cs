using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite;

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

    public Database(string connectionString = "Data Source=insurance.db")
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
            using var connection = new SqliteConnection(_connectionString);
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
        ITableInitializer[] tables = new ITableInitializer[6];
        tables[0] = new Type_of_object_Table(_connectionString);
        tables[1] = new Type_of_insurance_Table(_connectionString);
        tables[2] = new Manager_Table(_connectionString);
        tables[3] = new Client_Table(_connectionString);
        tables[4] = new Object_Table(_connectionString);
        tables[5] = new Insurance_contract_Table(_connectionString);

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var pragma = connection.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON;";
        pragma.ExecuteNonQuery();

        using var transaction = connection.BeginTransaction();

        foreach (var table in tables)
        {
            try
            {
                table.EnsureCreated(connection, transaction);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new InvalidOperationException($"Не удалось создать таблицу {table.TableName}", ex);
            }
        }

        transaction.Commit();
    }
}
