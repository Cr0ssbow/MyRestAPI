using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite;

public class Type_of_insurance_Table : IType_of_insurance
{
    private readonly string _connectionString;

    public Type_of_insurance_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Type_of_insurance (
                Type_id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Description TEXT NOT NULL,
                Price NUMERIC NOT NULL,
                Insurance_month INTEGER NOT NULL
            );
        """;
        command.ExecuteNonQuery();
    }

    public List<Type_of_insurance> Get() => throw new NotImplementedException();
    public Type_of_insurance? Get(int id) => throw new NotImplementedException();
    public void Post(Type_of_insurance item) => throw new NotImplementedException();
    public void Put(int id, Type_of_insurance item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
}
