using System.Threading.Tasks;

namespace MvcWebLibrary
{
    public interface IHttpRequestListener
    {
        IConfiguration Configuration { get; set; }
        Task StartListening();
    }
}