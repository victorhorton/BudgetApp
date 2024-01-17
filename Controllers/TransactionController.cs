using Microsoft.AspNetCore.Mvc;
using BudgetApp.Models;
using BudgetApp.Data;
using Microsoft.EntityFrameworkCore;
using BudgetApp.Requests;
using BudgetApp.DTOs;

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
        var transactions = await _dataContext.Transactions
            .Include(transaction => transaction.ItemTransactions)
            .ToListAsync();

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
    public async Task<ActionResult<Transaction>> PostTransaction(TransactionCreationDto transactionDto)
    {
        // Create new Transaction entity from DTO
        var transaction = new Transaction
        {
            Date = transactionDto.Date,
            Type = transactionDto.Type,
            Vendor = transactionDto.Vendor,
            Amount = transactionDto.Amount,
            Description = transactionDto.Description,
            Number = transactionDto.Number
        };

        // Add Transaction to database context
        _dataContext.Transactions.Add(transaction);
        await _dataContext.SaveChangesAsync();

        // Loop through each Item ID in the DTO and create associations in the junction table
        if (transactionDto.ItemIds != null)
        {
            foreach (var newItemId in transactionDto.ItemIds)
            {
                var itemTransaction = new ItemTransaction
                {
                    TransactionId = transaction.Id,  // Assuming Id is the primary key of Transaction
                    ItemId = newItemId
                };

                // Assuming you have a DbSet for ItemTransaction in your DbContext
                _dataContext.ItemTransactions.Add(itemTransaction);
            }

            // Save changes to the database
            await _dataContext.SaveChangesAsync();
        }

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

    [HttpPost("add-relationship")]
    public async Task<IActionResult> AddRelationship([FromBody] AddItemToTransactionRequest request)
    {
        var item = await _dataContext.Items.FindAsync(request.ItemId);
        var transaction = await _dataContext.Transactions.FindAsync(request.TransactionId);

        if (item == null)
        {
            return NotFound("Item not found");
        }

        if (transaction == null)
        {
            return NotFound("Transaction not found");
        }

        var itemTransaction = new ItemTransaction
        {
            ItemId = request.ItemId,
            TransactionId = request.TransactionId,
        };

        _dataContext.ItemTransactions.Add(itemTransaction);
        await _dataContext.SaveChangesAsync();

        return NoContent();
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
    
    [HttpDelete("delete-relationship")]
    public IActionResult DeleteRelationship([FromBody] ItemTransactionDTO itemTransactionDTO)
    {
        try
        {
            // Retrieve the transaction and item from the database based on IDs
            var transaction = _dataContext.Transactions.Find(itemTransactionDTO.TransactionId);
            var item = _dataContext.Items.Find(itemTransactionDTO.ItemId);

            if (transaction == null || item == null)
            {
                return NotFound("Transaction or item not found.");
            }

            // Check if the relationship exists
            var relationship = _dataContext.ItemTransactions
                .SingleOrDefault(ti => ti.TransactionId == transaction.Id && ti.ItemId == item.Id);

            if (relationship == null)
            {
                return NotFound("Relationship not found.");
            }

            // Remove the relationship
            _dataContext.ItemTransactions.Remove(relationship);
            _dataContext.SaveChanges();

            return Ok("Relationship deleted successfully.");
        }
        catch (Exception ex)
        {
            // Handle exceptions as needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
