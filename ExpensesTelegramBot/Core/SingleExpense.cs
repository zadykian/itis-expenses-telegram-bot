using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class SingleExpense : ValueObject
    {
        public string Category { get; set; }
        public int Amount { get; set; }
    }
}
