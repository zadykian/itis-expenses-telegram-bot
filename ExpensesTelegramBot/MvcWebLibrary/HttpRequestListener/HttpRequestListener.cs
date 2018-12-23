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
        private readonly IConfiguration configuration;
        private readonly HttpListener httpListener;

        public HttpRequestListener(IHttpRequestHandler requestHandler, IConfiguration configuration)
        {
            this.requestHandler = requestHandler ?? 
                throw new ArgumentNullException(nameof(requestHandler));

            this.configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));

            httpListener = new HttpListener();
            httpListener.Prefixes.Add(configuration.GetToken("ListeningUrl"));
        }

        //public void UseConfiguration(IConfiguration configuration)
        //{
        //    if (configuration == null)
        //    {
        //        this.configuration = configuration;
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException(
        //            "Configuration cannot be set more than once.");
        //    }
        //}

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
                var actionResult = requestHandler.Handle(httpContext.Request);
                await actionResult.ExecuteResultAsync(httpContext);
            }
        }
    }
}