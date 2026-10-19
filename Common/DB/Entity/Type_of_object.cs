using System.Text.RegularExpressions;

namespace MyRestAPI.Common.DB.Entity;

public class Type_of_object
{
    private string type = string.Empty;
    private const string typePattern = @"^[А-Яа-я0-9\- ]{3,50}$";

    public int Type_of_object_id { get; set; }

    public string Type
    {
        get { return type; }
        set
        {
            if (new Regex(typePattern).IsMatch(value))
                type = value;
            else
                throw new ArgumentException("Not valid type of object");
        }
    }
}