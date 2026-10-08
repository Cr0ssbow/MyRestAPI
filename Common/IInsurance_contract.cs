namespace APP.Common;

public interface IInsurance_contract
{
  int Contract_id { get; set; }
  int Client_id { get; set; }
  int Type_id { get; set; }
  int Object_id { get; set; }
  int Manager_id { get; set; }
  DateTime Date_of_conclusion { get; set; }
  DateTime Start_contract_date { get; set; }
  DateTime End_contract_date { get; set; }
  int Policy_cost { get; set; }
  int Coverage_amount { get; set; }
  string Status { get; set; }
}