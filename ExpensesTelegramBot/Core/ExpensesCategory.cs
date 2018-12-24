using System.Collections.Generic;
using System;

namespace Core
{
    public class ExpensesCategory : IValueObject, IEquatable<ExpensesCategory>
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return false;
            return Equals(obj as ExpensesCategory);
        }

        public bool Equals(ExpensesCategory other)
        {
            if (other == null) return false;
            return User.Equals(other.User) 
                && Category.Equals(other.Category);
        }

        public override int GetHashCode()
            => User.GetHashCode() ^ Category.GetHashCode();
    }
}
