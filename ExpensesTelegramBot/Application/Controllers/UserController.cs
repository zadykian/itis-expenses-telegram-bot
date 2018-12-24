using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Infrastructure;
using Core;
using MvcWebLibrary;
using System.Linq;

namespace Application
{
    public class UserController : DbAccessController
    {
        public UserController(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult AddSingleExpense(SingleExpense singleExpense)
        {
            dbContext.SingleExpenses.Add(singleExpense);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetStatistics(StatisticResult statisticResult)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult AddExpensesCategory(ExpensesCategory expensesCategory)
        {
            if (dbContext.ExpensesCategories.Find(expensesCategory) == null)
            {
                dbContext.ExpensesCategories.Add(expensesCategory);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult RemoveExpensesCategory(ExpensesCategory expensesCategory)
        {
            dbContext.ExpensesCategories.Remove(expensesCategory);
            return Ok();
        }
    }
}