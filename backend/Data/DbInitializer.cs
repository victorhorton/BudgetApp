using backend.Models;

namespace backend.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {

            if (context.Budgets.Any())
            {
                return;   // DB has been seeded
            }

            var budgets = new Budget[]
            {
                new Budget
                    { 
                        Month = "January", 
                    },
                new Budget
                    { 
                        Month = "Febuary", 
                    },
                new Budget
                    { 
                        Month = "March", 
                    },
            };

            context.Budgets.AddRange(budgets);
            context.SaveChanges();
        }
    }
}