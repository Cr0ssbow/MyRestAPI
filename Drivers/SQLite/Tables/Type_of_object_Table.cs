using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite.Tables;

public class Type_of_object_Table : IType_of_object
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Type_of_object (
            Type_of_object_id INTEGER PRIMARY KEY AUTOINCREMENT,
            Type TEXT NOT NULL
        );
        """;

    private readonly SqliteExecutor<Type_of_object> _executor;

    public Type_of_object_Table(SqliteExecutor<Type_of_object> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public List<Type_of_object> Get() => throw new NotImplementedException();
    public Type_of_object? Get(int id) => throw new NotImplementedException();
    public void Post(Type_of_object item) => throw new NotImplementedException();
    public void Put(int id, Type_of_object item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
}

