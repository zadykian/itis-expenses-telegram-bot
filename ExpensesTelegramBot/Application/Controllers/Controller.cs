using System;
using Infrastructure;

namespace Application
{
    public abstract class Controller : IDisposable
    {
        protected readonly IUnitOfWork unitOfWork;

        protected Controller(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

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