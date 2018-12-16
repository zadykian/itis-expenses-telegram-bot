using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;

namespace Application
{
    public class SecutiryController : DbAccessController
    {
        public SecutiryController(ApplicationContext dbContext)
            : base(dbContext)
        {
        }
    }
}
