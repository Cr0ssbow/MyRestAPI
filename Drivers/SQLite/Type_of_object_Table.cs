using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite;

public class Type_of_object_Table : IType_of_object
{
    private readonly string _connectionString;

    public Type_of_object_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Type_of_object (
                Type_of_object_id INTEGER PRIMARY KEY AUTOINCREMENT,
                Type TEXT NOT NULL
            );
        """;
        command.ExecuteNonQuery();
    }

    public List<Type_of_object> Get() => throw new NotImplementedException();
    public Type_of_object? Get(int id) => throw new NotImplementedException();
    public void Post(Type_of_object item) => throw new NotImplementedException();
    public void Put(int id, Type_of_object item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
}
