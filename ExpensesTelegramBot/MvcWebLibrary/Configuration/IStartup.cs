namespace MvcWebLibrary
{
    public interface IStartup
    {
        IConfiguration Configuration { get; }

        void ConfigureServices(IServiceConfigurator serviceConfigurator);
    }
}