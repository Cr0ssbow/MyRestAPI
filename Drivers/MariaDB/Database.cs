using System.Data;
using MySqlConnector;
using MyRestAPI.Common.DB;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Database : IDatabase
{
    private readonly string _connectionString;
    private readonly DbConnectionWrapper<MySqlConnection> _dbWrapper;

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
        _dbWrapper = new DbConnectionWrapper<MySqlConnection>(connectionString);

        Clients = new Client_Table(_dbWrapper);
        Managers = new Manager_Table(_dbWrapper);
        Contracts = new Insurance_contract_Table(_dbWrapper);
        Objects = new Object_Table(_dbWrapper);
        InsuranceTypes = new Type_of_insurance_Table(_dbWrapper);
        ObjectTypes = new Type_of_object_Table(_dbWrapper);
    }

    public bool TestConnection()
    {
        try
        {
            return _dbWrapper.ExecuteRead(conn => conn.State == ConnectionState.Open);
        }
        catch
        {
            return false;
        }
    }

    public void Initialize()
    {
        ITableInitializer[] tables = new ITableInitializer[6];
        tables[0] = (ITableInitializer)ObjectTypes;
        tables[1] = (ITableInitializer)InsuranceTypes;
        tables[2] = (ITableInitializer)Managers;
        tables[3] = (ITableInitializer)Clients;
        tables[4] = (ITableInitializer)Objects;
        tables[5] = (ITableInitializer)Contracts;

        _dbWrapper.ExecuteWrite(connection =>
        {
            foreach (var table in tables)
            {
                try
                {
                    table.EnsureCreated(connection);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Не удалось создать таблицу {table.TableName}", ex);
                }
            }
        });
    }

    public void Release()
    {
        _dbWrapper.Release();
    }

    public void Dispose()
    {
        Release();
    }
}
