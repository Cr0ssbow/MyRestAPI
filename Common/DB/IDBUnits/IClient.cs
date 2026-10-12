using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IdbUnits;

public interface IClient
{
    List<Client> Get();
    Client? Get(int id);
    void Post(Client item);
    void Put(int id, Client item);
    void Delete(int id);

    Client? GetByPassport(string series, string number); // Поиск клиента по паспортным данным

    Client? GetByPhone(string phoneNumber); // Поиск клиента по номеру телефона

    List<Client> SearchByName(string namePart); // Нечёткий поиск клиентов по совпадению части ФИО
}