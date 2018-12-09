using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Infrastructure;
using Core;

namespace Application
{
    public class UserController : Controller
    {
        public UserController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public IActionResult AddSingleExpense(SingleExpense newSingleExpense)
        {
            unitOfWork.SingleExpenseRepository.Add(newSingleExpense);
            return new ContentResult("Ok!");
        }

        
    }
}