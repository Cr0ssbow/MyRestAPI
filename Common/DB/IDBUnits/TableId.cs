namespace MyRestAPI.Common.DB.IDBUnits;

public enum TableId
{
    Type_of_object,
    Type_of_insurance,
    Manager,
    Client,
    Object,
    Insurance_contract
}

public static class TableOrder
{
    public static IReadOnlyList<TableId> Creation { get; } = Array.AsReadOnly(new[]
    {
        TableId.Type_of_object,
        TableId.Type_of_insurance,
        TableId.Manager,
        TableId.Client,
        TableId.Object,
        TableId.Insurance_contract
    });
}

