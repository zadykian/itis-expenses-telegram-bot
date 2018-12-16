using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Application
{
    public interface IRequestHandler
    {
        IActionResult Handle(HttpListenerContext httpContext);
    }
}