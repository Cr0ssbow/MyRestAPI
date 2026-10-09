namespace Common.DB.IdbUnits;

public interface IClient
{
    int ClientId { get; set; }
    string FullName { get; set; }
    string PassportSeries { get; set; }
    string PassportNumber { get; set; }
    DateOnly BirthDate { get; set; }
    string PhoneNumber { get; set; }
    string Address { get; set; }
    string? Email { get; set; }
}