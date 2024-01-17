namespace BudgetApp.Models;

public class ItemTransaction
{
    public int ItemId { get; set; }
    public Item? Item { get; set; }

    public int TransactionId { get; set; }
    public Transaction? Transaction { get; set; }

    public decimal Amount { get; set; }
}