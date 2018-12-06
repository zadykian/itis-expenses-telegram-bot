using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace Infrastructure
{
    public interface ISingleExpenseRepository
    {
        void Add(SingleExpense singleExpence);
        IEnumerable<SingleExpense> GetWhere(Func<SingleExpense, bool> predicate);
        void RemoveWhere(Func<SingleExpense, bool> predicate);
    }
}
