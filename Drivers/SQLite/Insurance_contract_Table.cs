using System.Data.Common;
using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite;

public class Insurance_contract_Table : IInsurance_contract, ITableInitializer
{
    private const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Insurance_contract (
            Contract_id INTEGER PRIMARY KEY AUTOINCREMENT,
            Client_id INTEGER NOT NULL,
            Type_id INTEGER NOT NULL,
            Object_id INTEGER NOT NULL,
            Manager_id INTEGER NOT NULL,
            Date_of_conclusion TEXT NOT NULL,
            Start_contract_date TEXT NOT NULL,
            End_contract_date TEXT NOT NULL,
            Policy_cost INTEGER NOT NULL,
            Coverage_amount INTEGER NOT NULL,
            Status TEXT NOT NULL,
            FOREIGN KEY (Client_id) REFERENCES Client(ClientId) ON DELETE RESTRICT,
            FOREIGN KEY (Type_id) REFERENCES Type_of_insurance(Type_id) ON DELETE RESTRICT,
            FOREIGN KEY (Object_id) REFERENCES Object(Object_id) ON DELETE RESTRICT,
            FOREIGN KEY (Manager_id) REFERENCES Manager(Manager_id) ON DELETE RESTRICT
        );
        """;

    private readonly string _connectionString;

    public string TableName => "Insurance_contract";

    public Insurance_contract_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool Exists(DbConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = @name;";
        var parameter = command.CreateParameter();
        parameter.ParameterName = "@name";
        parameter.Value = TableName;
        command.Parameters.Add(parameter);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public void EnsureCreated(DbConnection connection)
    {
        if (Exists(connection))
        {
            return;
        }

        using var command = connection.CreateCommand();
        command.CommandText = CreateSql;
        command.ExecuteNonQuery();
    }

    public List<Insurance_contract> Get() => throw new NotImplementedException();
    public Insurance_contract? Get(int id) => throw new NotImplementedException();
    public void Post(Insurance_contract item) => throw new NotImplementedException();
    public void Put(int id, Insurance_contract item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
    public List<Insurance_contract> GetByClientId(int clientId) => throw new NotImplementedException();
    public List<Insurance_contract> GetByManagerId(int managerId) => throw new NotImplementedException();
    public List<Insurance_contract> GetByStatus(string status) => throw new NotImplementedException();
    public void UpdateStatus(int id, string newStatus) => throw new NotImplementedException();
    public List<Insurance_contract> GetDealsReport(DateTime fromDate, DateTime toDate, int? managerId = null, string? status = null) => throw new NotImplementedException();
    public List<Insurance_contract> GetExpiringContracts(int days) => throw new NotImplementedException();
}
