using Microsoft.AspNetCore.Mvc;
using BudgetApp.Models;
using BudgetApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
     private readonly DataContext _dataContext;

    public CategoryController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    // GET: api/category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _dataContext.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound(); // Return 404 if category is not found
        }

        return category;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        _dataContext.Categories.Add(category);
        await _dataContext.SaveChangesAsync();

        // Return 201 Created status code and the created category
        // You can use CreatedAtAction to return a 201 status code and the newly created resource
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    // PUT: api/categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory)
    {
        _dataContext.Update(updatedCategory);
        await _dataContext.SaveChangesAsync();

        return NoContent(); // 204 No Content response upon successful update
    }
}
