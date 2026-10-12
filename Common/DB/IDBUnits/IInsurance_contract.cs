using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IdbUnits;

public interface IInsurance_contract
{
    List<Insurance_contract> Get();
    Insurance_contract? Get(int id);
    void Post(Insurance_contract item);
    void Put(int id, Insurance_contract item);
    void Delete(int id);

    List<Insurance_contract> GetByClientId(int clientId); // Выборка всех договоров конкретного клиента

    List<Insurance_contract> GetByManagerId(int managerId); // Выборка всех договоров оформленных определенным менеджером

    List<Insurance_contract> GetByStatus(string status); // Фильтрация договоров по статусу Активен, Завершён, Расторгнут

    void UpdateStatus(int id, string newStatus); // Быстрое изменение статуса договора завершение или расторжение

    List<Insurance_contract> GetDealsReport(DateTime fromDate, DateTime toDate, int? managerId = null, string? status = null); // Отчет О совершенных сделках: реестр сделок за выбранный период по менеджерам и статусам

    List<Insurance_contract> GetExpiringContracts(int days); // Контроль сроков действия список страховок, истекающих в ближайшие ? дней
}