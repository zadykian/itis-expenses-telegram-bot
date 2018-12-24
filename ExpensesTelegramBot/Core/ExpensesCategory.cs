using System.Collections.Generic;
using System;

namespace Core
{
    public class ExpensesCategory : IValueObject
    {
        private ExpensesCategory()
        {
        }
        
        public ExpensesCategory(User user, string category)
        {
            User = user ?? 
                throw new ArgumentNullException(nameof(user));

            UserLogin = User.Login;

            Category = category?.ToLowerInvariant() ??
                throw new ArgumentNullException(nameof(category));
        }

        public string UserLogin { get; private set; }

        public User User { get; private set; }

        public string Category { get; private set; }
    }
}
