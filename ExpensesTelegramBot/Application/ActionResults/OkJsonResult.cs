using Newtonsoft.Json;

namespace Application
{
    public class OkJsonResult<T> : OkResult
        where T : class
    {
        private readonly T instance;

        public OkJsonResult(T instance)
        {
            this.instance = instance;
        }

        public override string ResultString 
            => JsonConvert.SerializeObject(instance);
    }
}