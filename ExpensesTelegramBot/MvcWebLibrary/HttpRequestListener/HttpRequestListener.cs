using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace MvcWebLibrary
{
    internal class HttpRequestListener : IHttpRequestListener
    {
        private readonly IHttpRequestHandler requestHandler;
        private readonly HttpListener httpListener;

        public HttpRequestListener((string, int) hostPortPair, IHttpRequestHandler requestHandler)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes
                .Add($"http://{hostPortPair.Item1}:{hostPortPair.Item2}/");

            this.requestHandler = requestHandler ?? 
                throw new ArgumentNullException(nameof(requestHandler));
        }

        public async void StartListening()
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
                await actionResult.ExecuteResultAsync(httpContext);
            }
        }
    }
}