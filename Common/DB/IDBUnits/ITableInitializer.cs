using System.Data.Common;

namespace MyRestAPI.Common.DB.IDBUnits;

public interface ITableInitializer
{
    string TableName { get; }                     // имя таблицы в БД
    bool Exists(DbConnection connection);         // есть ли таблица в БД
    void EnsureCreated(DbConnection connection);  // если таблицы нет, создать её
}

