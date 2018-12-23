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

        [HttpPost]
        public IActionResult AddSingleExpense(SingleExpense singleExpense)
        {
            dbContext.SingleExpenses.Add(singleExpense);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetStatistics(StatisticRequest statisticRequest)
        {
            var categoriesToAmounts = dbContext.SingleExpenses
        }
    }
}