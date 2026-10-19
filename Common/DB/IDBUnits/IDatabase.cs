namespace MyRestAPI.Common.DB.IDBUnits;

public interface IDataBase
{
    IClient Clients { get; }
    IManager Managers { get; }
    IInsurance_contract Contracts { get; }
    IObject Objects { get; }
    IType_of_insurance InsuranceTypes { get; }
    IType_of_object ObjectTypes { get; }
    IDBUnits Units { get; }

    // Проверка доступности подключения к базе данных MariaDB
    bool TestConnection();
}
