using System;

namespace Application
{
    public static class ArgsParser
    {
        public static (string, int) GetHostAndPort(string[] args)
        {
            if (args.Length == 2 && int.TryParse(args[1], out int port))
            {
                return (args[0], port);
            }
            else throw new ArgumentException(nameof(args));
        }
    }
}