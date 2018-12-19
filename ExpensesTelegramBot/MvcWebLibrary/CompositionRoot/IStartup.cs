namespace MvcWebLibrary
{
    public interface IStartup
    {
        void ConfigureServices(IServiceConfigurator serviceConfigurator);
    }
}