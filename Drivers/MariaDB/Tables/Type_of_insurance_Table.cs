using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB.Tables;

public class Type_of_insurance_Table : IType_of_insurance
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Type_of_insurance (
            Type_id INT AUTO_INCREMENT PRIMARY KEY,
            Name VARCHAR(50) NOT NULL,
            Description VARCHAR(500) NOT NULL,
            Price DECIMAL(10, 2) NOT NULL,
            Insurance_month INT NOT NULL
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;

    private readonly MariaDbExecutor<Type_of_insurance> _executor;

    public Type_of_insurance_Table(MariaDbExecutor<Type_of_insurance> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public List<Type_of_insurance> Get() => throw new NotImplementedException();
    public Type_of_insurance? Get(int id) => throw new NotImplementedException();
    public void Post(Type_of_insurance item) => throw new NotImplementedException();
    public void Put(int id, Type_of_insurance item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
}

