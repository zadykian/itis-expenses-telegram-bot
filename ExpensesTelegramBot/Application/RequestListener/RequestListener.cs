﻿using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace Application
{
    public class RequestListener : IRequestListener
    {
        private readonly IRequestHandler requestHandler;
        private readonly HttpListener httpListener;

        public RequestListener((string, int) hostPortPair, IRequestHandler requestHandler)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes
                .Add($"http://{hostPortPair.Item1}:{hostPortPair.Item2}/");

            this.requestHandler = requestHandler ?? 
                throw new ArgumentNullException(nameof(requestHandler));
        }

        public void StartListening()
        {
            if (httpListener.IsListening)
            {
                return;
            }
            httpListener.Start();
            while (true)
            {
                var httpContext = httpListener.GetContext();
                var actionResult = requestHandler.Handle(httpContext);
                actionResult.ExecuteResultAsync(httpContext);
            }
        }
    }
}