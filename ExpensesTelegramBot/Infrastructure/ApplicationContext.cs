using Microsoft.EntityFrameworkCore;
using Core;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        private readonly string connectionString;

        public ApplicationContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; private set; }

        public DbSet<SingleExpense> SingleExpenses { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}