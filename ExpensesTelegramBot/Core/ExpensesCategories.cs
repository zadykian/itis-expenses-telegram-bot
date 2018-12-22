using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class ExpensesCategories : ValueObject
    {
        private ExpensesCategories()
        {
        }


        public int UserId { get; private set; }

        public List<string> Categories { get; private set; }

        
    }
}
