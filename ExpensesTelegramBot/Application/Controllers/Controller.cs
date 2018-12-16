using System;
using Infrastructure;

namespace Application
{
    public abstract class ControllerBase : IDisposable
    {
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
        }

        protected JsonResult<T> Json<T>(T instanceToSerialize)
            => new JsonResult<T>(instanceToSerialize);

        protected ContentResult Content(string content)
            => new ContentResult(content);
    }
}