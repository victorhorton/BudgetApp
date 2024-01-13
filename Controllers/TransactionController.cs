using Microsoft.AspNetCore.Mvc;
using BudgetApp.Models;
using BudgetApp.Data;

namespace BudgetApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
     private readonly DataContext _dataContext;

    public TransactionController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    // GET: api/transaction/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        var transaction = await _dataContext.Transactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound(); // Return 404 if transaction is not found
        }

        return transaction;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
    {
        _dataContext.Transactions.Add(transaction);
        await _dataContext.SaveChangesAsync();

        // Return 201 Created status code and the created transaction
        // You can use CreatedAtAction to return a 201 status code and the newly created resource
        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
    }

    // PUT: api/transactions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(int id, Transaction updatedTransaction)
    {
        _dataContext.Update(updatedTransaction);
        await _dataContext.SaveChangesAsync();

        return NoContent(); // 204 No Content response upon successful update
    }
}
