using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class WebHost
    {
        private readonly ICompositionRoot compositionRoot;
        private readonly IRequestListener requestListener;

        public WebHost(ICompositionRoot compositionRoot, IRequestListener requestListener)
        {
            this.compositionRoot = compositionRoot ?? 
                throw new ArgumentNullException(nameof(compositionRoot));

            this.requestListener = requestListener ?? 
                throw new ArgumentNullException(nameof(requestListener));
        }

        public static WebHost CreateDefault(string[] args)
        {
            var compositionRoot = new CompositionRoot();
            compositionRoot.ConfigureServices();
            var requestHandler = compositionRoot.GetRequestHandler();
            var listener = new RequestListener(GetHostAndPort(args), requestHandler);     
            return new WebHost(compositionRoot, listener);
        }

        public void Run() => requestListener.StartListening();

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