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

        var transactionDtos = transactions.Select(t => new TransactionDto
        {
            Id = t.Id,
            Date = t.Date,
            Type = t.Type,
            Vendor = t.Vendor,
            Amount = t.Amount,
            Description = t.Description,
            Number = t.Number,
            ItemIds = t.ItemIds  // Use the custom property in Transaction model to get Item IDs
        }).ToList();

        return Ok(transactionDtos);
        }

    // GET: api/transaction/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
    {
        var transaction = await _dataContext.Transactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound(); // Return 404 if transaction is not found
        }

        var transactionDto = new TransactionDto
        {
            Id = transaction.Id,
            Date = transaction.Date,
            Type = transaction.Type,
            Vendor = transaction.Vendor,
            Amount = transaction.Amount,
            Description = transaction.Description,
            Number = transaction.Number,
            ItemIds = transaction.ItemIds  // Use the custom property in Transaction model to get Item IDs
        };

        return transactionDto;
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
            foreach (var itemId in transactionDto.ItemIds)
            {
                var itemTransaction = new ItemTransaction
                {
                    TransactionId = transaction.Id,  // Assuming Id is the primary key of Transaction
                    ItemId = itemId
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
    public async Task<IActionResult> UpdateTransaction(int id, Transaction updatedTransaction)
    {
        _dataContext.Update(updatedTransaction);
        await _dataContext.SaveChangesAsync();

        return NoContent(); // 204 No Content response upon successful update
    }

    // POST api/transactions/add-items
    [HttpPost("add-items")]
    public async Task<IActionResult> AddItemsToTransaction([FromBody] AddItemsToTransactionRequest request)
    {
        if (request == null || request.TransactionId == 0 || request.ItemIds == null || request.ItemIds.Count == 0)
        {
            return BadRequest("Invalid request parameters");
        }

        var transaction = await _dataContext.Transactions.FindAsync(request.TransactionId);

        if (transaction == null)
        {
            return NotFound("Transaction not found");
        }

        foreach (var itemId in request.ItemIds)
        {
            var item = await _dataContext.Items.FindAsync(itemId);

            if (item == null)
            {
                continue; // or return NotFound($"Item with ID {itemId} not found");
            }

            var itemTransaction = new ItemTransaction
            {
                ItemId = itemId,
                TransactionId = request.TransactionId
            };

            _dataContext.ItemTransactions.Add(itemTransaction);
        }

        await _dataContext.SaveChangesAsync();

        return Ok("Items added to transaction successfully");
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
