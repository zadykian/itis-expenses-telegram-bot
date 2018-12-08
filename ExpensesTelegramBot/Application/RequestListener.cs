using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace Application
{
    public class RequestListener
    {
        private readonly HttpListener httpListener;
        private bool isListening;

        public RequestListener((string, int) hostPortPair)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(
                $"http://{hostPortPair.Item1}:{hostPortPair.Item2}/");
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
                var context =  httpListener.GetContext();
                var response = context.Response;
                var request = context.Request;

                var buffer = Encoding.UTF8.GetBytes("AAAAAAAA");
                    
                response.ContentLength64 = buffer.Length;

                

                using (Stream stream = response.OutputStream)
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}