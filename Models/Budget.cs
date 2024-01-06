namespace BudgetApp.Models;

public class Budget
{
    public int Id { get; set; }

    public string? Month { get; set; }
     public ICollection<Category> Categories { get; } = new List<Category>();

}