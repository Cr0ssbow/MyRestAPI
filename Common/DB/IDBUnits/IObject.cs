using MyAPP.Common.DB.Entity;
using Object = MyAPP.Common.DB.Entity.Object;

namespace MyAPP.Common.DB.IdbUnits;

public interface IObject
{
    // Базовые CRUD-операции
    List<Object> Get();
    Object? Get(int id);
    void Post(Object item);
    void Put(int id, Object item);
    void Delete(int id);

    Object? GetByIdentifier(string identifier); // Поиск объекта по идентификатору VIN, госномер, кадастровый номер

    List<Object> GetByTypeId(int typeOfObjectId); // Фильтрация объектов страхования по типу объекта
}