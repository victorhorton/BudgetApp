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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Budget>()
            .HasMany(b => b.Categories)
            .WithOne(c => c.Budget)
            .HasForeignKey(c => c.BudgetId);

        modelBuilder.Entity<Category>()
            .HasMany(b => b.Items)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId);

        modelBuilder.Entity<Item>()
            .HasMany(b => b.Transactions)
            .WithOne(c => c.Item)
            .HasForeignKey(c => c.ItemId);
    }
}