using System;

namespace MvcWebLibrary
{

    [Serializable]
    public class ModelBindingException : ApplicationException
    {
        public ModelBindingException() { }
        public ModelBindingException(string message) : base(message) { }
        public ModelBindingException(string message, Exception inner) : base(message, inner) { }
        protected ModelBindingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}