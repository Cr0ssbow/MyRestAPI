using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite;

public class Manager_Table : IManager
{
    private readonly string _connectionString;

    public Manager_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Manager (
                Manager_id INTEGER PRIMARY KEY AUTOINCREMENT,
                Full_Manager_Name TEXT NOT NULL,
                Post TEXT NOT NULL,
                Phone_number TEXT NOT NULL,
                Email TEXT NOT NULL
            );
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
