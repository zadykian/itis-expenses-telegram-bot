using MvcWebLibrary;
using Infrastructure;

namespace Application
{
    public class Startup : IStartup
    {
        public void ConfigureServices(IServiceConfigurator serviceConfigurator)
        {
            serviceConfigurator.AddParentScopeService<ApplicationContext>();
        }
    }
}