using System;
using System.Net;

namespace MvcWebLibrary
{
    internal class HttpRequestListener : IHttpRequestListener
    {
        private IConfiguration configuration;
        private readonly IHttpRequestHandler requestHandler;
        private readonly HttpListener httpListener;

        public HttpRequestListener(IHttpRequestHandler requestHandler)
        {
            this.requestHandler = requestHandler ?? 
                throw new ArgumentNullException(nameof(requestHandler));
            httpListener = new HttpListener();
        }

        public IConfiguration Configuration
        {
            get { return configuration; }
            set
            {
                if (configuration == null) configuration = value;
                else throw new InvalidOperationException(
                    "Configuration cannot be set more than once.");
            }
        }

        public async void StartListening()
        {
            if (httpListener.IsListening)
            {
                return;
            }
            if (configuration == null)
            {
                throw new InvalidOperationException(
                    "Configiration is null. Please specify configuration via Configuration property.");
            }
            var listeningUrl = configuration.GetToken("ListeningUrl");
            if (listeningUrl == null)
            {
                throw new ArgumentException("Configuration does not contain required field 'ListeningUrl'.");
            }
            httpListener.Prefixes.Add(listeningUrl);
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