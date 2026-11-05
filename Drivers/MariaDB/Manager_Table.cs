using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Manager_Table : IManager
{
    private readonly string _connectionString;

    public Manager_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(MySqlConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Manager (
                Manager_id INT AUTO_INCREMENT PRIMARY KEY,
                Full_Manager_Name VARCHAR(150) NOT NULL,
                Post VARCHAR(50) NOT NULL,
                Phone_number VARCHAR(15) NOT NULL,
                Email VARCHAR(100) NOT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;
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
