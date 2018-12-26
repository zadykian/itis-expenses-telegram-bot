using System;

namespace MvcWebLibrary
{
    public abstract class ControllerBase : IDisposable
    {
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
        }

        protected OkResult Ok() => new OkResult();

        protected BadRequestResult BadRequest() => new BadRequestResult();
        protected UnauthorizedResult Unauthorized() => new UnauthorizedResult();
        protected ForbiddenResult Forbidden() => new ForbiddenResult();
        protected NotFoundResult NotFound() => new NotFoundResult();

        protected JsonResult<T> Json<T>(T instanceToSerialize)
            => new JsonResult<T>(instanceToSerialize);

        protected ContentResult Content(string content)
            => new ContentResult(content);
    }
}