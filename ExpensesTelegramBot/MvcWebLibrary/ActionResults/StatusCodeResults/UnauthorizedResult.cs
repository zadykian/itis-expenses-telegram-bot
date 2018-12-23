using System;
using System.Collections.Generic;
using System.Text;

namespace MvcWebLibrary
{
    public class UnauthorizedResult : StatusCodeResult
    {
        public override int StatusCode => 401;
    }
}
