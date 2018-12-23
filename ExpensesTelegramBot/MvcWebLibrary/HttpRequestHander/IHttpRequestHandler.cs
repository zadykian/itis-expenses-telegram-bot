using System.Net;

namespace MvcWebLibrary
{
    public interface IHttpRequestHandler
    {
        IActionResult Handle(HttpListenerRequest httpRequest);
    }
}