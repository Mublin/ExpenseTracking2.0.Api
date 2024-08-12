using ExpenseTracking2._0.Api.Data;
using ExpenseTracking2._0.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking2._0.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    private readonly DbContextHandler _context;
    public CategoryController (DbContextHandler context)
    {
        _context = context; 
    }


    [HttpPost]
    public async Task<ActionResult<Category>> Category (Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpGet]
    public async Task<IEnumerable<Category>> Categories() 
    {
        return await _context.Categories.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> CategoryDetail(int id)
    {
        var category = await _context.Categories
        .Include(c => c.Expenses) // Eager load the related Expenses
        .SingleOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        var categoryDetails = new Category
        {
            Id = category.Id,
            Name = category.Name,
            Expenses = category.Expenses.Select(e => new Expense
            {
                Id = e.Id,
                Title = e.Title, // Assuming there's a Description property
                Amount = e.Amount
               
            }).ToList()
        };
        return Ok(categoryDetails);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> CategoryById(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c=> c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> CategoryId(int id, Category category)
    {
        var ECategory = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        if (ECategory == null)
        {
            return NotFound();
        }
        _context.Categories.Entry(ECategory).CurrentValues.SetValues(category);
        await _context.SaveChangesAsync();
        return Ok("Updated Successfully");
    }
}
