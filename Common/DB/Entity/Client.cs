namespace APP.Common;

public class Client : IClient
{
    public int Client_id { get ; set ; }
    public string Full_name { get ; set ; }
    public int Passport_siries { get ; set ; }
    public int Passport_number { get ; set ; }
    public DateTime Date_born { get ; set ; }
    public string Phone_number { get ; set ; }
    public string Adres { get ; set ; }
    public string Email { get ; set ; }
}