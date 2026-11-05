using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Type_of_insurance_Table : IType_of_insurance
{
    private readonly string _connectionString;

    public Type_of_insurance_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(MySqlConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Type_of_insurance (
                Type_id INT AUTO_INCREMENT PRIMARY KEY,
                Name VARCHAR(50) NOT NULL,
                Description VARCHAR(500) NOT NULL,
                Price DECIMAL(10, 2) NOT NULL,
                Insurance_month INT NOT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;
        command.ExecuteNonQuery();
    }

    public List<Type_of_insurance> Get() => throw new NotImplementedException();
    public Type_of_insurance? Get(int id) => throw new NotImplementedException();
    public void Post(Type_of_insurance item) => throw new NotImplementedException();
    public void Put(int id, Type_of_insurance item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
}
