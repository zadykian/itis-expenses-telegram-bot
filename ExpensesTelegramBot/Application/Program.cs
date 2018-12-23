using MvcWebLibrary;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.CreateDefault()
                .UseStartup<Startup>()
                .Run();
        }
    }
}