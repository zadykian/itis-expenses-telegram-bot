using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class WebHost<TStartup> where TStartup : class, new()
    {
        private readonly TStartup startup;
        private readonly IRequestListener requestListener;

        public WebHost(IRequestListener requestListener)
        {
            startup = new TStartup();
            this.requestListener = requestListener;
        }

        public static WebHost<TStartup> CreateDefault(string[] args)
        {
            var listener = new RequestListener(GetHostAndPort(args));
            return new WebHost<TStartup>(listener);
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