namespace APP.Common;

public interface IObject
{
  int Object_id { get; set; }
  int Type_of_object_id { get; set; }
  string Title { get; set; }
  string Identifier { get; set; }
  string Description { get; set; }
  int Estimated_value { get; set; }
}