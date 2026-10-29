using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;
using Object = MyRestAPI.Common.DB.Entity.Object;

namespace MyRestAPI.Drivers.SQLite;

public class Object_Table : IObject
{
    private readonly string _connectionString;

    public Object_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Object (
                Object_id INTEGER PRIMARY KEY AUTOINCREMENT,
                Type_of_object_id INTEGER NOT NULL,
                Title TEXT NOT NULL,
                Identifier TEXT NOT NULL,
                Description TEXT NOT NULL,
                Estimated_value INTEGER NOT NULL,
                FOREIGN KEY (Type_of_object_id) REFERENCES Type_of_object(Type_of_object_id) ON DELETE RESTRICT
            );
        """;
        command.ExecuteNonQuery();
    }

    public List<Object> Get() => throw new NotImplementedException();
    public Object? Get(int id) => throw new NotImplementedException();
    public void Post(Object item) => throw new NotImplementedException();
    public void Put(int id, Object item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
    public Object? GetByIdentifier(string identifier) => throw new NotImplementedException();
    public List<Object> GetByTypeId(int typeOfObjectId) => throw new NotImplementedException();
}
