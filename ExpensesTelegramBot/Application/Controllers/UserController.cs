using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Infrastructure;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult AddSingleExpense(SingleExpense newSingleExpense)
        {
            //unitOfWork.SingleExpenseRepository.Add(newSingleExpense);
            return new ContentResult("Ok!");
        }      
    }
}