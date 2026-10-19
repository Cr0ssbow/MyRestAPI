using MyRestAPI.Common.DB.Entity;

namespace MyRestAPI.Common.DB.IDBUnits;

public interface IType_of_object
{
    List<Type_of_object> Get();
    Type_of_object? Get(int id);
    void Post(Type_of_object item);
    void Put(int id, Type_of_object item);
    void Delete(int id);
}