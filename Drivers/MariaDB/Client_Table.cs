using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Client_Table : IClient
{
    private readonly string _connectionString;

    public Client_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(MySqlConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Client (
                ClientId INT AUTO_INCREMENT PRIMARY KEY,
                FullName VARCHAR(150) NOT NULL,
                PassportSeries VARCHAR(4) NOT NULL,
                PassportNumber VARCHAR(6) NOT NULL,
                BirthDate DATE NOT NULL,
                PhoneNumber VARCHAR(15) NOT NULL,
                Address VARCHAR(200) NOT NULL,
                Email VARCHAR(100) NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;
        command.ExecuteNonQuery();
    }

    public List<Client> Get() => throw new NotImplementedException();
    public Client? Get(int id) => throw new NotImplementedException();
    public void Post(Client item) => throw new NotImplementedException();
    public void Put(int id, Client item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
    public Client? GetByPassport(string series, string number) => throw new NotImplementedException();
    public Client? GetByPhone(string phoneNumber) => throw new NotImplementedException();
    public List<Client> SearchByName(string namePart) => throw new NotImplementedException();
}
