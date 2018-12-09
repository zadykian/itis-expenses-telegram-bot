using System.Net;
using Infrastructure;

namespace Application
{
    public class RequestHandler
    {
        private readonly IRouter router;
        private readonly IUnitOfWork unitOfWork;
        private readonly IModelBinder modelBinder;

        public RequestHandler(
            IRouter router, 
            IUnitOfWork unitOfWork,
            IModelBinder modelBinder)
        {
            this.router = router;
            this.unitOfWork = unitOfWork;
            this.modelBinder = modelBinder;
        }

        public IActionResult Handle(HttpListenerContext httpContext)
        {
            var controllerType = router
                .GetControllerType(httpContext.Request);

            var controllerAction = router
                .GetControllerAction(httpContext.Request, controllerType);

            var controllerInstance = controllerType
                .GetConstructor(new[] { typeof(IUnitOfWork) })
                .Invoke(new[] { unitOfWork });

            var actionParams = modelBinder
                .BindArguments(httpContext.Request, controllerAction);

            var result = controllerAction
                .Invoke(controllerInstance, actionParams);

            return result as IActionResult;
        }
    }
}