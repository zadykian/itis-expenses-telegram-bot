using System;

namespace MvcWebLibrary
{
    public class WebHost
    {
        private readonly ICompositionRoot compositionRoot;
        private readonly IHttpRequestListener requestListener;

        public WebHost(ICompositionRoot compositionRoot, IHttpRequestListener requestListener)
        {
            this.compositionRoot = compositionRoot ?? 
                throw new ArgumentNullException(nameof(compositionRoot));

            this.requestListener = requestListener ?? 
                throw new ArgumentNullException(nameof(requestListener));
        }

        public static WebHost CreateDefault(string[] args)
        {
            var compositionRoot = new NinjectCompositionRoot();
            var requestHandler = compositionRoot.GetHttpRequestHandler();
            var listener = new HttpRequestListener(GetHostAndPort(args), requestHandler);     
            return new WebHost(compositionRoot, listener);
        }

        public WebHost UseStartup<TStartup>() where TStartup : IStartup, new()
        {
            var startup = new TStartup();
            var serviceConfigurator = compositionRoot.ServiceConfigurator;
            startup.ConfigureServices(serviceConfigurator);
            return this;
        }

        public void Run()
        {
            requestListener.StartListening();
            Console.ReadKey();
        }

        private static (string, int) GetHostAndPort(string[] args)
        {
            if (args.Length == 2 && int.TryParse(args[1], out int port))
            {
                return (args[0], port);
            }
            else throw new ArgumentException(nameof(args));
        }
    }
}