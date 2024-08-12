using ExpenseTracking2._0.Api.Data;
using ExpenseTracking2._0.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking2._0.Api.Controllers;
[ApiController]
[Route("[controller]")]
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
}
