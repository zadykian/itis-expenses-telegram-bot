using Newtonsoft.Json.Linq;

namespace MvcWebLibrary
{
    public class Configuration : IConfiguration
    {
        private readonly JObject jsonObject;

        public Configuration(JObject jsonObject)
        {
            this.jsonObject = jsonObject;
        }

        public string GetToken(string key) => jsonObject[key].ToString();
    }
}
