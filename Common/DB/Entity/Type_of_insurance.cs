namespace APP.Common;

public class Type_of_insurace : IType_of_insurance
{
    public int Type_id { get ; set ; }
    public string Name { get ; set ; }
    public string Description { get ; set ; }
    public decimal price { get ; set ; }
    public int Insurance_month { get ; set ; }
}