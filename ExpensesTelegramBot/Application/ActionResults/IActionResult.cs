using System.Threading.Tasks;
using System.Net;

namespace Application
{
    public interface IActionResult
    {
        Task ExecuteResultAsync(HttpListenerContext context);
    }
}