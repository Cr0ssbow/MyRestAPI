using System.Data.Common;
using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Client_Table : IClient, ITableInitializer
{
    private const string CreateSql = """
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

    private readonly string _connectionString;

    public string TableName => "Client";

    public Client_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool Exists(DbConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = @name;";
        var parameter = command.CreateParameter();
        parameter.ParameterName = "@name";
        parameter.Value = TableName;
        command.Parameters.Add(parameter);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public void EnsureCreated(DbConnection connection)
    {
        if (Exists(connection))
        {
            return;
        }

        using var command = connection.CreateCommand();
        command.CommandText = CreateSql;
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
