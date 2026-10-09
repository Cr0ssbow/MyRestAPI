namespace APP.Common;

public class Insurance_contract : IInsurance_contract
{
    public int Contract_id { get ; set ; }
    public int Client_id { get ; set ; }
    public int Type_id { get ; set ; }
    public int Object_id { get ; set ; }
    public int Manager_id { get ; set ; }
    public DateTime Date_of_conclusion { get ; set ; }
    public DateTime Start_contract_date { get ; set ; }
    public DateTime End_contract_date { get ; set ; }
    public int Policy_cost { get ; set ; }
    public int Coverage_amount { get ; set ; }
    public string Status { get ; set ; }
}