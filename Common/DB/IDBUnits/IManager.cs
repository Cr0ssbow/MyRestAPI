using MyRestAPI.Common.DB.Entity;

namespace MyRestAPI.Common.DB.IDBUnits;

public interface IManager
{
    List<Manager> Get();
    Manager? Get(int id);
    void Post(Manager item);
    void Put(int id, Manager item);
    void Delete(int id);

    Manager? GetByEmail(string email); // Поиск сотрудника по мылу

    List<Manager> GetByPost(string post); // Выборка сотрудников по должности
}