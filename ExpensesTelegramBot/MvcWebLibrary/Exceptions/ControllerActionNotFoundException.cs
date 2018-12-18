using System;

namespace MvcWebLibrary
{
    [Serializable]
    public class ControllerActionNotFoundException : ApplicationException
    {
        public ControllerActionNotFoundException() { }
        public ControllerActionNotFoundException(string message) : base(message) { }
        public ControllerActionNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ControllerActionNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}