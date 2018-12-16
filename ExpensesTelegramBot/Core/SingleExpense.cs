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

        public SingleExpense(string category, int amount)
        {
            Category = category;
            Amount = amount;
        }

        public string Category { get; private set; }
        public int Amount { get; private set; }
    }
}
