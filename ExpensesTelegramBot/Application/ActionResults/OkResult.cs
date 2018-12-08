using System.Net;
using System.Text;

namespace Application
{
    public abstract class OkResult : ActionResult
    {
        public abstract string ResultString { get; }

        public override void ExecuteResult(HttpListenerContext context)
        {
            context.Response.StatusCode = 200;
            var result = Encoding.UTF8.GetBytes(ResultString);
            context.Response.OutputStream.Write(result, 0, result.Length);
        }
    }
}