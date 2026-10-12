using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IdbUnits;

public interface IType_of_insurance
{
    List<Type_of_insurance> Get();
    Type_of_insurance? Get(int id);
    void Post(Type_of_insurance item);
    void Put(int id, Type_of_insurance item);
    void Delete(int id);
}