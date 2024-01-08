using Microsoft.AspNetCore.Mvc;
using BudgetApp.Models;
using BudgetApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetController : ControllerBase
{
     private readonly DataContext _dataContext;

    public BudgetController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<Budget>> GetBudgets()
    {
        var budget = await _dataContext.Budgets
            .Include(budget => budget.Categories)
            .ThenInclude(category => category.Items)
            .SingleOrDefaultAsync(x => x.Id == 2);

        if (budget == null)
        {
            return new Budget(); // Return 404 if budget is not found
        }

        return budget;
    }

    // GET: api/budget/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Budget>> GetBudget(int id)
    {
        var budget = await _dataContext.Budgets.FindAsync(id);

        if (budget == null)
        {
            return NotFound(); // Return 404 if budget is not found
        }

        return budget;
    }

    [HttpPost]
    public async Task<ActionResult<Budget>> PostBudget(Budget budget)
    {
        _dataContext.Budgets.Add(budget);
        await _dataContext.SaveChangesAsync();

        // Return 201 Created status code and the created budget
        // You can use CreatedAtAction to return a 201 status code and the newly created resource
        return CreatedAtAction(nameof(GetBudget), new { id = budget.Id }, budget);
    }
}
