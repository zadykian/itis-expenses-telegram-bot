using System;
using System.Reflection;

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

        public static WebHost CreateDefault()
        {
            var compositionRoot = new NinjectCompositionRoot(Assembly.GetCallingAssembly());
            var requestHandler = compositionRoot.GetHttpRequestHandler();
            var listener = new HttpRequestListener(requestHandler, null /*IConfiguration*/);     
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
    }
}