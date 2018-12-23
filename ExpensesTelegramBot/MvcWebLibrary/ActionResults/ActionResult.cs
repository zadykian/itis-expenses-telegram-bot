using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        protected static void WriteIntoBody(string content, HttpListenerResponse httpResponse)
        {
            var result = Encoding.UTF8.GetBytes(content);
            httpResponse.ContentLength64 = result.Length;
            using (Stream stream = httpResponse.OutputStream)
            {
                stream.Write(result, 0, result.Length);
            }
        }
    }
}