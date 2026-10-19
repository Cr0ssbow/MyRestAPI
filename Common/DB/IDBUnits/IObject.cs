using MyRestAPI.Common.DB.Entity;
using Object = MyRestAPI.Common.DB.Entity.Object;

namespace MyRestAPI.Common.DB.IDBUnits;

public interface IObject
{
    List<Object> Get();
    Object? Get(int id);
    void Post(Object item);
    void Put(int id, Object item);
    void Delete(int id);

    Object? GetByIdentifier(string identifier); // Поиск объекта по идентификатору VIN, госномер, кадастровый номер

    List<Object> GetByTypeId(int typeOfObjectId); // Фильтрация объектов страхования по типу объекта
}