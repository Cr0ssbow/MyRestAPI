using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite;

public class Database : IDataBase
{
    private static readonly Dictionary<TableId, Action<SqliteConnection>> creators = new()
    {
        [TableId.Type_of_object] = Type_of_object_Table.EnsureTableCreated,
        [TableId.Type_of_insurance] = Type_of_insurance_Table.EnsureTableCreated,
        [TableId.Manager] = Manager_Table.EnsureTableCreated,
        [TableId.Client] = Client_Table.EnsureTableCreated,
        [TableId.Object] = Object_Table.EnsureTableCreated,
        [TableId.Insurance_contract] = Insurance_contract_Table.EnsureTableCreated
    };

    private readonly string _connectionString;

    public IClient Clients { get; }
    public IManager Managers { get; }
    public IInsurance_contract Contracts { get; }
    public IObject Objects { get; }
    public IType_of_insurance InsuranceTypes { get; }
    public IType_of_object ObjectTypes { get; }
    public IDBUnits Units => throw new NotImplementedException();

    public Database(string connectionString = "Data Source=insurance.db")
    {
        _connectionString = connectionString;

        Clients = new Client_Table(connectionString);
        Managers = new Manager_Table(connectionString);
        Contracts = new Insurance_contract_Table(connectionString);
        Objects = new Object_Table(connectionString);
        InsuranceTypes = new Type_of_insurance_Table(connectionString);
        ObjectTypes = new Type_of_object_Table(connectionString);
    }

    public bool TestConnection()
    {
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Initialize()
    {
        foreach (var id in TableOrder.Creation)
        {
            if (!creators.TryGetValue(id, out var creator) || creator is null)
            {
                throw new InvalidOperationException($"Не зарегистрирован создатель для таблицы {id}");
            }
        }

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Включение поддержки внешних ключей в SQLite
        using var pragma = connection.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON;";
        pragma.ExecuteNonQuery();

        using var transaction = connection.BeginTransaction();

        foreach (var id in TableOrder.Creation)
        {
            try
            {
                creators[id](connection);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new InvalidOperationException($"Не удалось создать таблицу {id}", ex);
            }
        }

        transaction.Commit();
    }
}
