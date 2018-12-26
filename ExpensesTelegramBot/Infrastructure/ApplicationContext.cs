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

        public DbSet<Channel> Channels { get; private set; }

        public DbSet<RegularExpensesCategory> RegularExpensesCategories { get; private set; }

        public DbSet<SingleExpense> SingleExpenses { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.SecretLogin);
            modelBuilder.Entity<RegularExpensesCategory>().HasIndex(expensesCat => expensesCat.UserSecretLogin);
            modelBuilder.Entity<SingleExpense>().HasIndex(expense => expense.ChannelId);

            modelBuilder.Entity<Channel>()
                .HasMany<SingleExpense>()
                .WithOne(exp => exp.Channel)
                .HasForeignKey(exp => exp.ChannelId);

            modelBuilder.Entity<User>()
                .HasMany<RegularExpensesCategory>()
                .WithOne(cat => cat.User)
                .HasForeignKey(cat => cat.UserSecretLogin);

            modelBuilder.Entity<User>()
                .HasMany<Channel>()
                .WithOne(channel => channel.User)
                .HasForeignKey(channel => channel.UserSecretLogin);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}