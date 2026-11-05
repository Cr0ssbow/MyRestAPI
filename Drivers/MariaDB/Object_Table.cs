using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;
using Object = MyRestAPI.Common.DB.Entity.Object;

namespace MyRestAPI.Drivers.MariaDB;

public class Object_Table : IObject
{
    private readonly string _connectionString;

    public Object_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(MySqlConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Object (
                Object_id INT AUTO_INCREMENT PRIMARY KEY,
                Type_of_object_id INT NOT NULL,
                Title VARCHAR(100) NOT NULL,
                Identifier VARCHAR(50) NOT NULL,
                Description VARCHAR(500) NOT NULL,
                Estimated_value INT NOT NULL,
                CONSTRAINT FK_Object_Type_of_object FOREIGN KEY (Type_of_object_id)
                    REFERENCES Type_of_object(Type_of_object_id) ON DELETE RESTRICT
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
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
