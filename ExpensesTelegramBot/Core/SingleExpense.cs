using System;

namespace Core
{
    public class SingleExpense : IValueObject
    {
        private SingleExpense()
        {
        }

        public SingleExpense(User user, string category, int amount)
        {
            Id = Guid.NewGuid();
            User = user ?? throw new ArgumentNullException(nameof(user));
            UserLogin = user.Login;
            Category = category.ToLowerInvariant();
            Amount = amount;
        }

        public Guid Id { get; private set; }

        public string UserLogin { get; private set; }

        public User User { get; private set; }

        public string Category { get; private set; }

        public int Amount { get; private set; }
    }
}