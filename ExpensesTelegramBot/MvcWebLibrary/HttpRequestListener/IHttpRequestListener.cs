namespace MvcWebLibrary
{
    public interface IHttpRequestListener
    {
        IConfiguration Configuration { get; set; }
        void StartListening();
    }
}