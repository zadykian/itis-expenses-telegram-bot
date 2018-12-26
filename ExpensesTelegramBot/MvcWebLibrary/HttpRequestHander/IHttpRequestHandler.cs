using System.Net;
using System.Threading.Tasks;

namespace MvcWebLibrary
{
    public interface IHttpRequestHandler
    {
        Task<IActionResult> HandleAsync(HttpListenerRequest httpRequest);
    }
}