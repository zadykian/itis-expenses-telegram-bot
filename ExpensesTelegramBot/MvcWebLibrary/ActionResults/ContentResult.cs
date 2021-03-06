﻿using System.Net;

namespace MvcWebLibrary
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
            WriteIntoBody(Content, context.Response);
            base.ExecuteResult(context);
        }
    }
}