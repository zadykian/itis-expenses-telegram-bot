using System;

namespace Application
{
    public abstract class Controller : IDisposable
    {
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}