﻿using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace Application
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
            var result = Encoding.UTF8.GetBytes(json);
            using (Stream stream = context.Response.OutputStream)
            {
                stream.Write(result, 0, result.Length);
            }
            base.ExecuteResult(context);
        }
    }
}