using System.Net;

namespace Application
{
    public class ContentResult : ActionResult
    {
        public ContentResult(string content)
        {
            Content = content;
        }

        public string Content { get; }

        public override void ExecuteResult(HttpListenerContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.Headers.Add("Content", Content);
            base.ExecuteResult(context);
        }
    }
}