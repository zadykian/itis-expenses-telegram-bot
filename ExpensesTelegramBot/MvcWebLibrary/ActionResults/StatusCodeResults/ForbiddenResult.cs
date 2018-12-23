using System;
using System.Collections.Generic;
using System.Text;

namespace MvcWebLibrary
{
    public class ForbiddenResult : StatusCodeResult
    {
        public override int StatusCode => 403;
    }
}
