using System.Net;
using System.Threading.Tasks;

namespace MvcWebLibrary
{
    public abstract class ActionResult : IActionResult
    {
        public virtual Task ExecuteResultAsync(HttpListenerContext context)
        {
            ExecuteResult(context);
            return Task.FromResult(true);
        }

        public virtual void ExecuteResult(HttpListenerContext context)
        {
            context.Response.Close();
        }
    }
}