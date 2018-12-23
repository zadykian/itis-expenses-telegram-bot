using System;
using System.Net;
using System.Reflection;

namespace MvcWebLibrary
{
    internal class HttpRequestHandler : IHttpRequestHandler
    {
        private readonly IRouter router;
        private readonly IModelBinder modelBinder;
        private readonly ICompositionRoot compositionRoot;

        public HttpRequestHandler(IRouter router, IModelBinder modelBinder, ICompositionRoot compositionRoot)
        {
            this.router = router;
            this.modelBinder = modelBinder;
            this.compositionRoot = compositionRoot;
        }

        public IActionResult Handle(HttpListenerRequest httpRequest)
        {
            var routePath = httpRequest.Url.AbsolutePath.TrimStart('/').Split('/');
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
                    .GetControllerAction(controllerType, routePath[1], httpRequest.HttpMethod);
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
                    .BindArguments(httpRequest, controllerAction);
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