using System.IO;
using Newtonsoft.Json.Linq;

namespace MvcWebLibrary
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private JObject jsonObject;
        private string basePath;

        public ConfigurationBuilder()
        {
            basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }

        public IConfigurationBuilder AddJsonFile(string fileName)
        {
            var filePath = $"{basePath}\\{fileName}";
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                var json = streamReader.ReadToEnd();
                jsonObject = JObject.Parse(json);
            }
            return this;
        }

        public IConfiguration Build() => new Configuration(jsonObject);

        public IConfigurationBuilder SetBasePath(string newBasePath)
        {
            basePath = newBasePath;
            return this;
        }
    }
}
