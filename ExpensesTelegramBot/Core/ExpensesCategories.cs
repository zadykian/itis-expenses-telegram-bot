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
        
        public ExpensesCategories(Guid userId, List<string> categories)
        {
            UserId = userId;
            Categories = categories;
            Categories.ForEach(str => str.ToLowerInvariant());
        }

        public Guid UserId { get; private set; }

        public List<string> Categories { get; private set; }     
    }
}
