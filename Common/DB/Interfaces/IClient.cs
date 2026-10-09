namespace APP.Common;

public interface IClient
{
  int Client_id { get; set; }
  string Full_name { get; set; }
  int Passport_siries { get; set; }
  int Passport_number { get; set; }
  DateTime Date_born { get; set; }
  string Phone_number { get; set; }
  string Adres { get; set; }
  string Email { get; set; }
}