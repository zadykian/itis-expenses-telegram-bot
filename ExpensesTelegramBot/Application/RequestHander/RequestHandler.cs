using System;
using System.Net;

namespace Application
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IRouter router;
        private readonly IModelBinder modelBinder;

        public RequestHandler(IRouter router, IModelBinder modelBinder)
        {
            this.router = router;
            this.modelBinder = modelBinder;
        }

        public IActionResult Handle(HttpListenerContext httpContext)
        {
            var controllerType = router
                .GetControllerType(httpContext.Request);

            var controllerAction = router
                .GetControllerAction(httpContext.Request, controllerType);

            var controllerInstance = Activator.CreateInstance(controllerType);

            var actionParams = modelBinder
                .BindArguments(httpContext.Request, controllerAction);

            var result = controllerAction
                .Invoke(controllerInstance, actionParams);

            return result as IActionResult;
        }
    }
}