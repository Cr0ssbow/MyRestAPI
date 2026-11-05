using MySqlConnector;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.MariaDB;

public class Insurance_contract_Table : IInsurance_contract
{
    private readonly string _connectionString;

    public Insurance_contract_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static void EnsureTableCreated(MySqlConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Insurance_contract (
                Contract_id INT AUTO_INCREMENT PRIMARY KEY,
                Client_id INT NOT NULL,
                Type_id INT NOT NULL,
                Object_id INT NOT NULL,
                Manager_id INT NOT NULL,
                Date_of_conclusion DATETIME NOT NULL,
                Start_contract_date DATETIME NOT NULL,
                End_contract_date DATETIME NOT NULL,
                Policy_cost INT NOT NULL,
                Coverage_amount INT NOT NULL,
                Status VARCHAR(20) NOT NULL,
                CONSTRAINT FK_Contract_Client FOREIGN KEY (Client_id)
                    REFERENCES Client(ClientId) ON DELETE RESTRICT,
                CONSTRAINT FK_Contract_Type FOREIGN KEY (Type_id)
                    REFERENCES Type_of_insurance(Type_id) ON DELETE RESTRICT,
                CONSTRAINT FK_Contract_Object FOREIGN KEY (Object_id)
                    REFERENCES Object(Object_id) ON DELETE RESTRICT,
                CONSTRAINT FK_Contract_Manager FOREIGN KEY (Manager_id)
                    REFERENCES Manager(Manager_id) ON DELETE RESTRICT
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;
        command.ExecuteNonQuery();
    }

    public List<Insurance_contract> Get() => throw new NotImplementedException();
    public Insurance_contract? Get(int id) => throw new NotImplementedException();
    public void Post(Insurance_contract item) => throw new NotImplementedException();
    public void Put(int id, Insurance_contract item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
    public List<Insurance_contract> GetByClientId(int clientId) => throw new NotImplementedException();
    public List<Insurance_contract> GetByManagerId(int managerId) => throw new NotImplementedException();
    public List<Insurance_contract> GetByStatus(string status) => throw new NotImplementedException();
    public void UpdateStatus(int id, string newStatus) => throw new NotImplementedException();
    public List<Insurance_contract> GetDealsReport(DateTime fromDate, DateTime toDate, int? managerId = null, string? status = null) => throw new NotImplementedException();
    public List<Insurance_contract> GetExpiringContracts(int days) => throw new NotImplementedException();
}
