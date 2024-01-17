using System.ComponentModel.DataAnnotations;
using BudgetApp.Converters;
using Newtonsoft.Json;

namespace BudgetApp.Requests;

public class TransactionRequest
{
    [JsonConverter(typeof(DateConverter))]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public string? Type { get; set; }

    public string? Vendor { get; set; }
    
    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public string? Number { get; set; }

    public required NewItem[] NewItems { get; set; }

}

public class NewItem
{
    [Key]
    public int ItemId { get; set; }
    public decimal SplitAmount { get; set; }
}