using System.Text.RegularExpressions;
using MyAPP.Common.DB.IdbUnits;

namespace MyAPP.Common.DB.Entity;

public class Manager
{
    private string full_Manager_Name = string.Empty;
    private const string full_Manager_NamePattern = @"^[А-Яа-я\- ]{5,150}$";

    private string post = string.Empty;
    private const string postPattern = @"^[А-Яа-я\- ]{3,50}$";

    private string phone_number = string.Empty;
    private const string phone_numberPattern = @"^\+?\d{10,15}$";

    private string email = string.Empty;
    private const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public int Manager_id { get; set; }

    public string Full_Manager_Name
    {
        get { return full_Manager_Name; }
        set
        {
            if (new Regex(full_Manager_NamePattern).IsMatch(value))
                full_Manager_Name = value;
            else
                throw new ArgumentException("Not valid manager name");
        }
    }

    public string Post
    {
        get { return post; }
        set
        {
            if (new Regex(postPattern).IsMatch(value))
                post = value;
            else
                throw new ArgumentException("Not valid post");
        }
    }

    public string Phone_number
    {
        get { return phone_number; }
        set
        {
            if (new Regex(phone_numberPattern).IsMatch(value))
                phone_number = value;
            else
                throw new ArgumentException("Not valid phone number");
        }
    }

    public string Email
    {
        get { return email; }
        set
        {
            if (new Regex(emailPattern).IsMatch(value))
                email = value;
            else
                throw new ArgumentException("Not valid email");
        }
    }
}