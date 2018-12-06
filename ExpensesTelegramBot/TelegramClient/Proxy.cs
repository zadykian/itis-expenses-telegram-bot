using MihaZupan;

namespace TelegramClient
{
    public static class Proxy
    {
        public static HttpToSocks5Proxy GetProxyIfNessesary()
        {
            // TODO: Ping telegram server (?)
            return new HttpToSocks5Proxy(
                "phobos.public.opennetwork.cc",
                1090,
                "340874765",
                "nHCHywRl")
            {
                ResolveHostnamesLocally = true
            };
        }
    }
}