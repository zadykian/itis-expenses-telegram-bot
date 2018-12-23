using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class SingleExpense : ValueObject
    {
        private SingleExpense()
        {
        }

        public SingleExpense(Guid userId, string category, int amount)
        {
            Category = category.ToLower();
            Amount = amount;
        }

        public Guid UserId { get; private set; }
        public string Category { get; private set; }
        public int Amount { get; private set; }
    }
}