using Microsoft.EntityFrameworkCore;
using BudgetApp.Models;

namespace BudgetApp.Data;

public class DataContext : DbContext
{
    public DataContext (DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories => Set<Category>();
}