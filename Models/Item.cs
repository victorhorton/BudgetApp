namespace BudgetApp.Models;

public class Item
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public decimal PlannedAmount { get; set; }

    public int CategoryId { get; set; } // Required foreign key property
    public Category? Category { get; set; }

    public ICollection<Transaction> Transactions { get; } = new List<Transaction>();

}