using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.DataTransferObjects
{
    public class ExpensesCategories
    {
        public ExpensesCategories(User user, List<string> categories)
        {
            User = user;
            Categories = categories;
        }

        public User User { get; private set; }

        public List<string> Categories { get; private set; }
    }
}
