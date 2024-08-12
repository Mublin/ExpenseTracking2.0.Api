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
[Route("api/[controller]")]
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
    [HttpPut("{id}")]
    public async Task<ActionResult<Expense>> ExpenseById(int id, Expense expense)
    {
        var Item = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id);
        if (Item == null)
        {
            return NotFound();
        }
        _context.Entry(Item).CurrentValues.SetValues(expense);
        await _context.SaveChangesAsync();
        return Ok();
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
        _context.Expenses.Remove(Item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
   
}
