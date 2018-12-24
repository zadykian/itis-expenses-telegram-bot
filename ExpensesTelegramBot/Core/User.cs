using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class User : IEntity, IEquatable<User>
    {
        private List<SingleExpense> singleExpenses;

        private List<ExpensesCategory> expensesCategories;

        private User()
        {
        }

        public User(string login, string password)
        {
            Login = login?.ToLowerInvariant() ??
                throw new ArgumentNullException(nameof(login));
            PasswordHash = password?.GetHashCode() ??
                throw new ArgumentNullException(nameof(password));
        }

        public string Login { get; private set; }

        public int PasswordHash { get; private set; }

        public IEnumerable<SingleExpense> SingleExpenses
            => singleExpenses.AsEnumerable();

        public IEnumerable<ExpensesCategory> ExpensesCategories
            => expensesCategories.AsEnumerable();

        public void AddSingleExpense(SingleExpense singleExpense)
            => singleExpenses.Add(singleExpense);

        public void RemoveLastSingleExpense()
            => singleExpenses.RemoveAt(singleExpenses.Count - 1);

        public void AddExpensesCategory(ExpensesCategory expensesCategory)
        {
            if (!expensesCategories.Contains(expensesCategory))
            {
                expensesCategories.Add(expensesCategory);
            }
        }

        public void RemoveExpensesCategory(ExpensesCategory expensesCategory)
        {
            expensesCategories.Remove(expensesCategory);
        }

        public void UpdatePassword(string newPassword)
        {
            PasswordHash = newPassword?.GetHashCode() ??
                throw new ArgumentNullException(nameof(newPassword));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            if (other == null) return false;
            return Login == other.Login;
        }

        public override int GetHashCode() => Login.GetHashCode();
    }
}