using System.Threading.Tasks;
using System.Net;

namespace MvcWebLibrary
{
    public interface IActionResult
    {
        Task ExecuteResultAsync(HttpListenerContext context);
    }
}