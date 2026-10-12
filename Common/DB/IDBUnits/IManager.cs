using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IdbUnits;

public interface IManager
{
    // Базовые CRUD-операции
    List<Manager> Get();
    Manager? Get(int id);
    void Post(Manager item);
    void Put(int id, Manager item);
    void Delete(int id);

    // Поиск сотрудника по корпоративной электронной почте
    Manager? GetByEmail(string email);

    // Выборка сотрудников по занимаемой должности
    List<Manager> GetByPost(string post);
}