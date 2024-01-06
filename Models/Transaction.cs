using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models;

public class Transaction
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public string? Vendor { get; set; }
    
    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public string? Number{ get; set; }

    public List<Item> Items { get; } = new();
}