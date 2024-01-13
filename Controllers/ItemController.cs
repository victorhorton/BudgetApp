using Microsoft.AspNetCore.Mvc;
using BudgetApp.Models;
using BudgetApp.Data;

namespace BudgetApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
     private readonly DataContext _dataContext;

    public ItemController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    // GET: api/category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(int id)
    {
        var item = await _dataContext.Items.FindAsync(id);

        if (item == null)
        {
            return NotFound(); // Return 404 if item is not found
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Item>> PostItem(Item item)
    {
        _dataContext.Items.Add(item);
        await _dataContext.SaveChangesAsync();

        // Return 201 Created status code and the created item
        // You can use CreatedAtAction to return a 201 status code and the newly created resource
        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
    }

    // PUT: api/items/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, Item updatedItem)
    {
        _dataContext.Update(updatedItem);
        await _dataContext.SaveChangesAsync();

        return NoContent(); // 204 No Content response upon successful update
    }
}
