using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IdbUnits;

public interface IClient
{
    List<Client> Get();
    Client? Get(int id);
    void Post(Client item);
    void Put(int id, Client item);
    void Delete(int id);

    // Поиск клиента по паспортным данным (серия и номер паспорта)
    Client? GetByPassport(string series, string number);

    // Быстрый поиск клиента по контактному номеру телефона
    Client? GetByPhone(string phoneNumber);

    // Нечёткий поиск клиентов по совпадению части ФИО
    List<Client> SearchByName(string namePart);
}