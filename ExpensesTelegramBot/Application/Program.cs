namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost<Startup>.CreateDefault(args)
                .Run();
        }
    }
}