using ExpenseTracking2._0.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking2._0.Api.Data;

public class DbContextHandler(DbContextOptions<DbContextHandler> options) : DbContext(options)
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }
}
