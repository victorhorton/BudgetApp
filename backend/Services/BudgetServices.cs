using backend.Models;

namespace backend.Services;

public static class BudgetService
{
    static List<Budget> Budgets { get; }
    static int nextId = 3;
    static BudgetService()
    {
        Budgets = new List<Budget>
        {
            new Budget { Id = 1, Month = "January" },
            new Budget { Id = 2, Month = "Febuary" }
        };
    }

    public static List<Budget> GetAll() => Budgets;

    public static Budget? Get(int id) => Budgets.FirstOrDefault(p => p.Id == id);

    public static void Add(Budget budget)
    {
        budget.Id = nextId++;
        Budgets.Add(budget);
    }

    public static void Delete(int id)
    {
        var Budget = Get(id);
        if(Budget is null)
            return;

        Budgets.Remove(Budget);
    }

    public static void Update(Budget budget)
    {
        var index = Budgets.FindIndex(p => p.Id == budget.Id);
        if(index == -1)
            return;

        Budgets[index] = budget;
    }
}