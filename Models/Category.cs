namespace BudgetApp.Models;

public class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public ICollection<Item> Items { get; } = new List<Item>();
    public int BudgetId { get; set; } // Required foreign key property
    public Budget? Budget { get; set; } 
}