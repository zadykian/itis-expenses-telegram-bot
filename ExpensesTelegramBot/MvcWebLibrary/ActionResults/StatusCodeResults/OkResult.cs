namespace MvcWebLibrary
{
    public class OkResult : StatusCodeResult
    {
        public override int StatusCode => 200;
    }
}