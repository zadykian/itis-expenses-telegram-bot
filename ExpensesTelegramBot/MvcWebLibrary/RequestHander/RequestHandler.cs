using System;
using System.Net;
using System.Reflection;

namespace MvcWebLibrary
{
    internal class RequestHandler : IRequestHandler
    {
        private readonly IRouter router;
        private readonly IModelBinder modelBinder;
        private readonly ICompositionRoot compositionRoot;

        public RequestHandler(IRouter router, IModelBinder modelBinder, ICompositionRoot compositionRoot)
        {
            this.router = router;
            this.modelBinder = modelBinder;
            this.compositionRoot = compositionRoot;
        }

        public IActionResult Handle(HttpListenerContext httpContext)
        {
            var routePath = httpContext.Request.Url.AbsolutePath.TrimStart('/').Split('/');
            if (routePath.Length != 2)
            {
                return new BadRequestResult();
            }

            Type controllerType;
            try
            {
                controllerType = router.GetControllerType(routePath[0]);
            }
            catch (ControllerNotFoundException)
            {
                return new NotFoundResult();
            }

            MethodInfo controllerAction;
            try
            {
                controllerAction = router
                    .GetControllerAction(controllerType, routePath[1], httpContext.Request.HttpMethod);
            }
            catch (ControllerActionNotFoundException)
            {
                return new NotFoundResult();
            }

            var controllerInstance = compositionRoot
                .GetControllerInstance(controllerType);

            object[] actionParams;
            try
            {
                actionParams = modelBinder
                    .BindArguments(httpContext.Request, controllerAction);
            }
            catch (ModelBindingException)
            {
                return new BadRequestResult();
            }

            var result = controllerAction
                .Invoke(controllerInstance, actionParams);

            return result as IActionResult;
        }
    }
}