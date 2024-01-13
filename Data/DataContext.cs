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
    public DbSet<ItemTransaction> ItemTransactions { get; set; }

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

        modelBuilder.Entity<ItemTransaction>()
            .HasKey(it => new { it.ItemId, it.TransactionId });

        modelBuilder.Entity<ItemTransaction>()
            .HasOne(it => it.Item)
            .WithMany(i => i.ItemTransactions)
            .HasForeignKey(it => it.ItemId);

        modelBuilder.Entity<ItemTransaction>()
            .HasOne(it => it.Transaction)
            .WithMany(t => t.ItemTransactions)
            .HasForeignKey(it => it.TransactionId);
    }
}