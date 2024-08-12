using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracking2._0.Api.Data;
using ExpenseTracking2._0.Api.Models;

namespace ExpenseTracking2._0.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseController : Controller
{
    private readonly DbContextHandler _context;

    public ExpenseController(DbContextHandler context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Expense>> Expenses()
    {
        return await _context.Expenses.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> Expense (int id) 
    {
        var Item = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id);
        if (Item == null) 
        {
            return NotFound();
        }
        return Ok(Item);
    }
    [HttpPost]
    public async Task<ActionResult<Expense>> Expense(Expense item) {
        _context.Expenses.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> ExpenseById(int id)
    {
        var Item = await _context.Expenses.SingleOrDefaultAsync(p => p.Id == id);
        if (Item == null) 
        {
            return NotFound();
        }
        _context.Expenses.Select(p => p.Id == id).ExecuteDelete();
        await _context.SaveChangesAsync();
        return NoContent();
    }
   
}
