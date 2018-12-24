using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class User : IEntity
    {
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

        public IEnumerable<SingleExpense> SingleExpenses => singleExpenses.AsEnumerable();
        public IEnumerable<ExpensesCategory> ExpensesCategories => expensesCategories.AsEnumerable();

        private List<SingleExpense> singleExpenses;
        private List<ExpensesCategory> expensesCategories;

        public void AddSingleExpense(SingleExpense singleExpense)
            => singleExpenses.Add(singleExpense);

        public void RemoveLastSingleExpense()
            => singleExpenses.RemoveAt(singleExpenses.Count - 1);

        public void UpdatePassword(string newPassword)
        {
            PasswordHash = newPassword?.GetHashCode() ??
                throw new ArgumentNullException(nameof(newPassword));
        }
    }
}