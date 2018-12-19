using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace MvcWebLibrary
{
    public interface IHttpRequestHandler
    {
        IActionResult Handle(HttpListenerContext httpContext);
    }
}