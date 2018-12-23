using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using MvcWebLibrary;
using Core;

namespace Application
{
    public class SecutiryController : DbAccessController
    {
        public SecutiryController(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

        [HttpPost]
        public IActionResult CreateNewUser(User user)
        {
            if (dbContext.Users.Find(user.Id) == null)
            {
                dbContext.Users.Add(user);
                return Ok();
            }
            else
            {
                return Forbidden();
            }
        }
    }
}
