using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;

namespace Application
{
    public class SecutiryController : Controller
    {
        public SecutiryController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
