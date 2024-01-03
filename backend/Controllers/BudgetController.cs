using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{
    public BudgetController()
    {
    }

    [HttpGet]
    public ActionResult<List<Budget>> GetAll() =>
        BudgetService.GetAll();

    // GET by Id action

    // POST action

    // PUT action

    // DELETE action
}