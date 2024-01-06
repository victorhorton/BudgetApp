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
    [HttpGet]
    public ActionResult<IEnumerable<Budget>> GetBudgets()
    {
        return _dataContext.Budgets
            .Include(budget => budget.Categories)
            .ThenInclude(category => category.Items)
            .ThenInclude(item => item.Transactions)
            .ToList();
    }
}
