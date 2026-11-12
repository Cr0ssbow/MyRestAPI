using System.Data.Common;
using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Type_of_insurance_Table : IType_of_insurance, ITableInitializer
{
    private const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Type_of_insurance (
            Type_id INT AUTO_INCREMENT PRIMARY KEY,
            Name VARCHAR(50) NOT NULL,
            Description VARCHAR(500) NOT NULL,
            Price DECIMAL(10, 2) NOT NULL,
            Insurance_month INT NOT NULL
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;

    private readonly string _connectionString;

    public string TableName => "Type_of_insurance";

    public Type_of_insurance_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool Exists(DbConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = @name;";
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

    public List<Type_of_insurance> Get() => throw new NotImplementedException();
    public Type_of_insurance? Get(int id) => throw new NotImplementedException();
    public void Post(Type_of_insurance item) => throw new NotImplementedException();
    public void Put(int id, Type_of_insurance item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
}
