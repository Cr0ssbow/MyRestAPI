using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite.Tables;

public class Type_of_insurance_Table : IType_of_insurance
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Type_of_insurance (
            Type_id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            Description TEXT NOT NULL,
            Price NUMERIC NOT NULL,
            Insurance_month INTEGER NOT NULL
        );
        """;

    private readonly SqliteExecutor<Type_of_insurance> _executor;

    public Type_of_insurance_Table(SqliteExecutor<Type_of_insurance> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public List<Type_of_insurance> Get() => throw new NotImplementedException();
    public Type_of_insurance? Get(int id) => throw new NotImplementedException();
    public void Post(Type_of_insurance item) => throw new NotImplementedException();
    public void Put(int id, Type_of_insurance item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
}

