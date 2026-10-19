using MyRestAPI.Common.DB.Entity;

namespace MyRestAPI.Common.DB.IDBUnits;

public interface IDBUnits
{
    List<Units> Get();
    Units? Get(int id);
    void Post(Units item);
    void Put(int id, Units item);
    void Delete(int id);
}