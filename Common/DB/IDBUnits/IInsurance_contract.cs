using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IdbUnits;

public interface IInsurance_contract
{
    // Базовые CRUD-операции
    List<Insurance_contract> Get();
    Insurance_contract? Get(int id);
    void Post(Insurance_contract item);
    void Put(int id, Insurance_contract item);
    void Delete(int id);

    // Выборка всех договоров конкретного клиента
    List<Insurance_contract> GetByClientId(int clientId);

    // Выборка всех договоров, оформленных определенным менеджером
    List<Insurance_contract> GetByManagerId(int managerId);

    // Фильтрация договоров по статусу (Активен, Завершён, Расторгнут)
    List<Insurance_contract> GetByStatus(string status);

    // Быстрое изменение статуса договора (завершение или расторжение)
    void UpdateStatus(int id, string newStatus);

    // Отчет «О совершенных сделках»: реестр сделок за выбранный период по менеджерам и статусам
    List<Insurance_contract> GetDealsReport(DateTime fromDate, DateTime toDate, int? managerId = null, string? status = null);

    // Контроль сроков действия: список страховок, истекающих в ближайшие N дней
    List<Insurance_contract> GetExpiringContracts(int days);
}