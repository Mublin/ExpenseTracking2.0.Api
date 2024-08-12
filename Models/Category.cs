﻿namespace ExpenseTracking2._0.Api.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ICollection<Expense> Expenses { get; set; } = [];
}