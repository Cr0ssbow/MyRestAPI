using System.Text.RegularExpressions;
using MyAPP.Common.DB.IdbUnits;

namespace MyAPP.Common.DB.Entity;

public class Object
{
    private int type_of_object_id;
    private const string type_of_object_idPattern = @"^[1-9]\d{0,8}$";

    private string title = string.Empty;
    private const string titlePattern = @"^[А-Яа-яA-Za-z0-9\-.,№ ]{2,100}$";

    private string identifier = string.Empty;
    private const string identifierPattern = @"^[A-Za-zА-Яа-я0-9\-/: ]{3,50}$";

    private string description = string.Empty;
    private const string descriptionPattern = @"^[\s\S]{0,500}$";

    private int estimated_value;
    private const string estimated_valuePattern = @"^[1-9]\d{0,9}$";

    public int Object_id { get; set; }

    public int Type_of_object_id
    {
        get { return type_of_object_id; }
        set
        {
            if (new Regex(type_of_object_idPattern).IsMatch(value.ToString()))
                type_of_object_id = value;
            else
                throw new ArgumentException("Not valid type of object id");
        }
    }

    public string Title
    {
        get { return title; }
        set
        {
            if (new Regex(titlePattern).IsMatch(value))
                title = value;
            else
                throw new ArgumentException("Not valid title");
        }
    }

    public string Identifier
    {
        get { return identifier; }
        set
        {
            if (new Regex(identifierPattern).IsMatch(value))
                identifier = value;
            else
                throw new ArgumentException("Not valid identifier");
        }
    }

    public string Description
    {
        get { return description; }
        set
        {
            if (new Regex(descriptionPattern).IsMatch(value))
                description = value;
            else
                throw new ArgumentException("Not valid description");
        }
    }

    public int Estimated_value
    {
        get { return estimated_value; }
        set
        {
            if (new Regex(estimated_valuePattern).IsMatch(value.ToString()))
                estimated_value = value;
            else
                throw new ArgumentException("Not valid estimated value");
        }
    }
}