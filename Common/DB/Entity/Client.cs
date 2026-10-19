using System.Text.RegularExpressions;

namespace MyRestAPI.Common.DB.Entity;

public class Client
{
    private string fullName = string.Empty;
    private const string fullNamePattern = @"^[А-Яа-я\- ]{5,150}$";

    private string passportSeries = string.Empty;
    private const string passportSeriesPattern = @"^\d{4}$";

    private string passportNumber = string.Empty;
    private const string passportNumberPattern = @"^\d{6}$";

    private DateOnly birthDate;

    private string phoneNumber = string.Empty;
    private const string phoneNumberPattern = @"^\+?\d{10,15}$";

    private string address = string.Empty;
    private const string addressPattern = @"^.{5,200}$";

    private string? email;
    private const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public int ClientId { get; set; }

    public string FullName
    {
        get { return fullName; }
        set
        {
            if (new Regex(fullNamePattern).IsMatch(value))
                fullName = value;
            else
                throw new ArgumentException("Not valid full name");
        }
    }

    public string PassportSeries
    {
        get { return passportSeries; }
        set
        {
            if (new Regex(passportSeriesPattern).IsMatch(value))
                passportSeries = value;
            else
                throw new ArgumentException("Not valid passport series");
        }
    }

    public string PassportNumber
    {
        get { return passportNumber; }
        set
        {
            if (new Regex(passportNumberPattern).IsMatch(value))
                passportNumber = value;
            else
                throw new ArgumentException("Not valid passport number");
        }
    }

    public DateOnly BirthDate
    {
        get { return birthDate; }
        set
        {
            if (value <= DateOnly.FromDateTime(DateTime.Today))
                birthDate = value;
            else
                throw new ArgumentException("Not valid birth date");
        }
    }

    public string PhoneNumber
    {
        get { return phoneNumber; }
        set
        {
            if (new Regex(phoneNumberPattern).IsMatch(value))
                phoneNumber = value;
            else
                throw new ArgumentException("Not valid phone number");
        }
    }

    public string Address
    {
        get { return address; }
        set
        {
            if (new Regex(addressPattern).IsMatch(value))
                address = value;
            else
                throw new ArgumentException("Not valid address");
        }
    }

    public string? Email
    {
        get { return email; }
        set
        {
            // email необязательный: null допустим
            if (value == null || new Regex(emailPattern).IsMatch(value))
                email = value;
            else
                throw new ArgumentException("Not valid email");
        }
    }
}