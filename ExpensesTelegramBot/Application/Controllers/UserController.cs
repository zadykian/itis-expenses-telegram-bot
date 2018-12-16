using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Infrastructure;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class UserController : DbAccessController
    {
        public UserController(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult AddSingleExpense(SingleExpense newSingleExpense)
        {
            dbContext.SingleExpenses.Add(newSingleExpense);
            return new ContentResult("Ok!");
        }      
    }
}