namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestListener.StartNew(ArgsParser.GetHostAndPort(args));
        }
    }
}
