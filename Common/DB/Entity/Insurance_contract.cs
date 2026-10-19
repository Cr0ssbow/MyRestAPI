using System.Text.RegularExpressions;

namespace MyRestAPI.Common.DB.Entity;

public class Insurance_contract
{
    private int client_id;
    private const string client_idPattern = @"^[1-9]\d{0,8}$";

    private int type_id;
    private const string type_idPattern = @"^[1-9]\d{0,8}$";

    private int object_id;
    private const string object_idPattern = @"^[1-9]\d{0,8}$";

    private int manager_id;
    private const string manager_idPattern = @"^[1-9]\d{0,8}$";

    private DateTime date_of_conclusion;
    private DateTime start_contract_date;
    private DateTime end_contract_date;

    private int policy_cost;
    private const string policy_costPattern = @"^[1-9]\d{0,8}$";

    private int coverage_amount;
    private const string coverage_amountPattern = @"^[1-9]\d{0,8}$";

    private string status = string.Empty;
    private const string statusPattern = "^(Активен|Завершён|Расторгнут)$";

    public int Contract_id { get; set; }

    public int Client_id
    {
        get { return client_id; }
        set
        {
            if (new Regex(client_idPattern).IsMatch(value.ToString()))
                client_id = value;
            else
                throw new ArgumentException("Not valid client id");
        }
    }

    public int Type_id
    {
        get { return type_id; }
        set
        {
            if (new Regex(type_idPattern).IsMatch(value.ToString()))
                type_id = value;
            else
                throw new ArgumentException("Not valid type id");
        }
    }

    public int Object_id
    {
        get { return object_id; }
        set
        {
            if (new Regex(object_idPattern).IsMatch(value.ToString()))
                object_id = value;
            else
                throw new ArgumentException("Not valid object id");
        }
    }

    public int Manager_id
    {
        get { return manager_id; }
        set
        {
            if (new Regex(manager_idPattern).IsMatch(value.ToString()))
                manager_id = value;
            else
                throw new ArgumentException("Not valid manager id");
        }
    }

    public DateTime Date_of_conclusion
    {
        get { return date_of_conclusion; }
        set
        {
            if (value.Date <= DateTime.Today)
                date_of_conclusion = value;
            else
                throw new ArgumentException("Not valid date of conclusion");
        }
    }

    public DateTime Start_contract_date
    {
        get { return start_contract_date; }
        set
        {
            if (date_of_conclusion != default && value.Date < date_of_conclusion.Date)
                throw new ArgumentException("Start date is earlier than date of conclusion");
            start_contract_date = value;
        }
    }

    public DateTime End_contract_date
    {
        get { return end_contract_date; }
        set
        {
            if (start_contract_date != default && value.Date <= start_contract_date.Date)
                throw new ArgumentException("End date must be later than start date");
            end_contract_date = value;
        }
    }

    public int Policy_cost
    {
        get { return policy_cost; }
        set
        {
            if (new Regex(policy_costPattern).IsMatch(value.ToString()))
                policy_cost = value;
            else
                throw new ArgumentException("Not valid policy cost");
        }
    }

    public int Coverage_amount
    {
        get { return coverage_amount; }
        set
        {
            if (new Regex(coverage_amountPattern).IsMatch(value.ToString()))
                coverage_amount = value;
            else
                throw new ArgumentException("Not valid coverage amount");
        }
    }

    public string Status
    {
        get { return status; }
        set
        {
            if (new Regex(statusPattern).IsMatch(value))
                status = value;
            else
                throw new ArgumentException("Not valid status");
        }
    }
}