using MvcWebLibrary;
using Infrastructure;

namespace Application
{
    public class Startup : IStartup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceConfigurator serviceConfigurator)
        {
            var connectionString = Configuration.GetToken("DbConnectionString");
            serviceConfigurator
                .AddParentScopedServiceWithConstructorArg<ApplicationContext>(connectionString);
        }
    }
}