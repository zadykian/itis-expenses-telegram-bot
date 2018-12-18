namespace MvcWebLibrary
{
    public class NotFoundResult : StatusCodeResult
    {
        public override int StatusCode => 404;
    }
}