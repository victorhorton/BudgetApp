using Microsoft.AspNetCore.Mvc;
using BudgetApp.Models;
using BudgetApp.Data;
using Microsoft.EntityFrameworkCore;
using BudgetApp.Requests;

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

    // GET: api/transaction
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
    {
        var transactions = await _dataContext.Transactions.ToListAsync();

        if (transactions.Count == 0)
        {
            return new List<Transaction>();
        }

        return Ok(transactions);
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

        // Add Transaction to database context
        _dataContext.Transactions.Add(transaction);
        await _dataContext.SaveChangesAsync();

        // Return 201 Created status code and the created transaction
        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
    }

    // PUT: api/transactions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(int id, Transaction transaction)
    {
        _dataContext.Update(transaction);
        await _dataContext.SaveChangesAsync();

        return NoContent(); // 204 No Content response upon successful update
    }
    
    // DELETE api/transactions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var transaction = await _dataContext.Transactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound();
        }

        _dataContext.Remove(transaction);
        await _dataContext.SaveChangesAsync();

        return NoContent(); // 204 No Content response upon successful update
    }
}
