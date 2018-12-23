using Newtonsoft.Json;
using System.Net;

namespace MvcWebLibrary
{
    public class JsonResult<T> : ActionResult
    {
        public JsonResult(T instance)
        {
            Instance = instance;
        }

        public T Instance { get; }

        public override void ExecuteResult(HttpListenerContext context)
        {
            context.Response.StatusCode = 200;
            var json = JsonConvert.SerializeObject(Instance);
            WriteIntoBody(json, context.Response);
            base.ExecuteResult(context);
        }
    }
}