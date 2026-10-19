using MyRestAPI.Common.DB.Entity;

namespace MyRestAPI.Common.DB.IDBUnits;

public interface IType_of_insurance
{
    List<Type_of_insurance> Get();
    Type_of_insurance? Get(int id);
    void Post(Type_of_insurance item);
    void Put(int id, Type_of_insurance item);
    void Delete(int id);
}