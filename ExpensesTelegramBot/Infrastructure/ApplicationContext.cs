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

        public DbSet<ExpensesCategory> UserExpensesCategories { get; private set; }

        public DbSet<SingleExpense> SingleExpenses { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.Login);
            modelBuilder.Entity<ExpensesCategory>().HasKey(record => record.UserLogin);
            modelBuilder.Entity<SingleExpense>().HasIndex(expense => expense.UserLogin);

            modelBuilder.Entity<User>()
                .HasMany<SingleExpense>()
                .WithOne(exp => exp.User)
                .HasForeignKey(exp => exp.UserLogin);

            modelBuilder.Entity<User>()
                .HasMany<ExpensesCategory>()
                .WithOne(cat => cat.User)
                .HasForeignKey(cat => cat.UserLogin);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}