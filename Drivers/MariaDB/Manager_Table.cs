using System.Data.Common;
using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Manager_Table : IManager, ITableInitializer
{
    private const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Manager (
            Manager_id INT AUTO_INCREMENT PRIMARY KEY,
            Full_Manager_Name VARCHAR(150) NOT NULL,
            Post VARCHAR(50) NOT NULL,
            Phone_number VARCHAR(15) NOT NULL,
            Email VARCHAR(100) NOT NULL
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;

    private readonly string _connectionString;

    public string TableName => "Manager";

    public Manager_Table(string connectionString)
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

    public List<Manager> Get() => throw new NotImplementedException();
    public Manager? Get(int id) => throw new NotImplementedException();
    public void Post(Manager item) => throw new NotImplementedException();
    public void Put(int id, Manager item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
    public Manager? GetByEmail(string email) => throw new NotImplementedException();
    public List<Manager> GetByPost(string post) => throw new NotImplementedException();
}
