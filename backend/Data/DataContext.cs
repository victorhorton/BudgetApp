using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DataContext : DbContext
    {
        // Define DbSets for your entities representing database tables
        public DbSet<Budget> Budgets { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            // Constructor to initialize DbContext with provided options (e.g., connection string, configurations)
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, constraints, indexes, and other configurations using Fluent API or annotations
            
            // Example: Configure a many-to-many relationship between Product and Order entities
            modelBuilder.Entity<Budget>();
            
            // ... other entity configurations, relationships, constraints, indexes, etc.
        }

        // Optionally, add methods, configurations, utilities, or overrides for specific database operations, optimizations, logging, etc.
    }
}
