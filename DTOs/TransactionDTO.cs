using System.ComponentModel.DataAnnotations;
using BudgetApp.Converters;
using Newtonsoft.Json;

namespace 
BudgetApp.DTOs;
public class TransactionDto
{
    public int Id { get; set; }

    [JsonConverter(typeof(DateConverter))]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public string? Type { get; set; }

    public string? Vendor { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public string? Number { get; set; }

    public List<int> ItemIds { get; set; }
}
