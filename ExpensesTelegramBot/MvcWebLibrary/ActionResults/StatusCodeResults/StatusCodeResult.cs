﻿using System.Net;

namespace MvcWebLibrary
{
    public abstract class StatusCodeResult : ActionResult
    {
        public abstract int StatusCode { get; }

        public override void ExecuteResult(HttpListenerContext context)
        {
            context.Response.StatusCode = StatusCode;
            base.ExecuteResult(context);
        }
    }
}