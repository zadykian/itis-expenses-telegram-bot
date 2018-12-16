using Microsoft.EntityFrameworkCore;
using Core;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; private set; }

        public DbSet<SingleExpense> SingleExpenses { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}