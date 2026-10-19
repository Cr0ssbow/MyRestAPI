using System.Text.RegularExpressions;

namespace MyRestAPI.Common.DB.Entity;

public class Type_of_insurance
{
    private string name = string.Empty;
    private const string namePattern = @"^[А-Яа-яЁё0-9\- ]{3,50}$";

    private string description = string.Empty;
    private const string descriptionPattern = @"^[\s\S]{0,500}$";

    private decimal price;
    private const string pricePattern = @"^[1-9]\d{0,7}([.,]\d{1,2})?$";

    private int insurance_month;
    private const string insurance_monthPattern = @"^([1-9]|[1-9]\d|1[01]\d|120)$";

    public int Type_id { get; set; }

    public string Name
    {
        get { return name; }
        set
        {
            if (new Regex(namePattern).IsMatch(value))
                name = value;
            else
                throw new ArgumentException("Not valid name");
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

    public decimal Price
    {
        get { return price; }
        set
        {
            if (new Regex(pricePattern).IsMatch(value.ToString()))
                price = value;
            else
                throw new ArgumentException("Not valid price");
        }
    }

    public int Insurance_month
    {
        get { return insurance_month; }
        set
        {
            if (new Regex(insurance_monthPattern).IsMatch(value.ToString()))
                insurance_month = value;
            else
                throw new ArgumentException("Not valid insurance month");
        }
    }
}