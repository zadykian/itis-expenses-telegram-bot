namespace MvcWebLibrary
{
    public class ForbiddenResult : StatusCodeResult
    {
        public override int StatusCode => 403;
    }
}
