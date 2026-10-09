namespace Common.DB.Entity;

using System.Text.RegularExpressions;
using Common.DB.IdbUnits;

public class Client : IClient
{
    public int ClientId { get; set; }
    private string fullName = string.Empty;
    private const string fullNamePattern = @"^[А-Яа-я ]{5,150}$";
    private string passportSeries = string.Empty;
    private const string passportSeriesPattern = @"^\d{4}$";
    private string passportNumber = string.Empty;
    private const string passportNumberPattern = @"^\d{6}$";
    public DateOnly BirthDate { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string Address { get; set; } = "";
    public string? Email { get; set; }

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
                throw new ArgumentException("Not valid passport series!");
        }
    }

    public string PassportNumber
    {
        get { return passportNumber; }
        set
        {
            if (new Regex(passportSeriesPattern).IsMatch(value))
                passportNumber = value;
            else
                throw new ArgumentException("Not valid passport series!");
        }
    }
}