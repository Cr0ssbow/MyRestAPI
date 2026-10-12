namespace APP.Common;

public interface IType_of_insurance
{
  int Type_id { get; set; }
  string Name { get; set; }
  string Description { get; set; }
  decimal price { get; set; }
  int Insurance_month { get; set; }
}