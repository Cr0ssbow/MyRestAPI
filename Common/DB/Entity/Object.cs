namespace APP.Common;

public class Object : IObject
{
    public int Object_id { get ; set ; }
    public int Type_of_object_id { get ; set ; }
    public string Title { get ; set ; }
    public string Identifier { get ; set ; }
    public string Description { get ; set ; }
    public int Estimated_value { get ; set ; }
}