using System;

namespace MvcWebLibrary
{
    [Serializable]
    public class ControllerNotFoundException : ApplicationException
    {
        public ControllerNotFoundException() { }
        public ControllerNotFoundException(string message) : base(message) { }
        public ControllerNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ControllerNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}