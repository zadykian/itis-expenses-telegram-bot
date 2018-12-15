using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace Application
{
    public class RequestListener : IRequestListener
    {
        private readonly HttpListener httpListener;

        public RequestListener((string, int) hostPortPair)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes
                .Add($"http://{hostPortPair.Item1}:{hostPortPair.Item2}/");
        }
        
        public async void StartListening()
        {
            httpListener.Start();
            while (true)
            {
                var httpContext = await httpListener.GetContextAsync();
                var requestHandler = new RequestHandler(
                    new Router(),
                    new ModelBinder());

                var actionResult = requestHandler.Handle(httpContext);
                await actionResult.ExecuteResultAsync(httpContext);
            }
        }
    }
}