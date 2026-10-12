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

    // Поиск объекта по уникальному идентификатору (VIN, госномер, кадастровый номер)
    Object? GetByIdentifier(string identifier);

    // Фильтрация объектов страхования по категории (типу объекта)
    List<Object> GetByTypeId(int typeOfObjectId);
}