using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Infrastructure;

namespace Application
{
    public class RequestListener
    {
        private readonly HttpListener httpListener;
        private bool isListening;

        public RequestListener((string, int) hostPortPair)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes
                .Add($"http://{hostPortPair.Item1}:{hostPortPair.Item2}/");
            httpListener.Start();
        }

        public static RequestListener StartNew((string, int) hostPortPair)
        {
            var listener = new RequestListener(hostPortPair);
            listener.StartListening();
            return listener; 
        }
        
        public void StartListening()
        {
            if (isListening) return;
            isListening = true;
            while (true)
            {
                var httpContext =  httpListener.GetContext();
                using (ApplicationContext dbContext = new ApplicationContext())
                {
                    var requestHandler = new RequestHandler(
                        new Router(),
                        new UnitOfWork(dbContext),
                        new ModelBinder());

                    var actionResult = requestHandler.Handle(httpContext);
                    actionResult.ExecuteResultAsync(httpContext);
                }
            }
        }
    }
}