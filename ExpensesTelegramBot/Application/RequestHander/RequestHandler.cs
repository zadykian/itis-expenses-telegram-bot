using System;
using System.Net;

namespace Application
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IRouter router;
        private readonly IModelBinder modelBinder;
        private readonly ICompositionRoot startup;

        public RequestHandler(IRouter router, IModelBinder modelBinder, ICompositionRoot startup)
        {
            this.router = router;
            this.modelBinder = modelBinder;
            this.startup = startup;
        }

        public IActionResult Handle(HttpListenerContext httpContext)
        {
            var controllerType = router
                .GetControllerType(httpContext.Request);

            var controllerAction = router
                .GetControllerAction(httpContext.Request, controllerType);

            var controllerInstance = startup.GetControllerInstance(controllerType);
            
            var actionParams = modelBinder
                .BindArguments(httpContext.Request, controllerAction);

            var result = controllerAction
                .Invoke(controllerInstance, actionParams);

            return result as IActionResult;
        }
    }
}