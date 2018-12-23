namespace MvcWebLibrary
{
    public interface IConfigurationBuilder
    {
        IConfigurationBuilder SetBasePath(string newBasePath);
        IConfigurationBuilder AddJsonFile(string fileName);
        IConfiguration Build();
    }
}
