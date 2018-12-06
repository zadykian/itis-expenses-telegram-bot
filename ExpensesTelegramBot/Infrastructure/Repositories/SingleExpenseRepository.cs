using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Infrastructure
{
    public class SingleExpenseRepository : ISingleExpenseRepository
    {
        private readonly ApplicationContext context;

        public SingleExpenseRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(SingleExpense newSingleExpence)
            => context.SingleExpenses.Add(newSingleExpence);

        public IEnumerable<SingleExpense> GetWhere(Func<SingleExpense, bool> predicate)
            => context.SingleExpenses.Where(predicate);

        public void RemoveWhere(Func<SingleExpense, bool> predicate)
        {
            var expensesToRemove = context.SingleExpenses
                .Where(predicate);
            context.SingleExpenses.RemoveRange(expensesToRemove);
        }
    }
}