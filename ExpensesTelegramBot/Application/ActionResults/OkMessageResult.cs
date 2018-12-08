namespace Application
{
    public class OkMessageResult : OkResult
    {
        public OkMessageResult(string message)
        {
            ResultString = message;
        }

        public override string ResultString { get; }
    }
}