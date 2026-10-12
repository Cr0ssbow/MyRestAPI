using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IdbUnits;

public interface IDBUnits
{
    List<Units> Get();
    Units? Get(int id);
    void Post(Units item);
    void Put(int id, Units item);
    void Delete(int id);
}