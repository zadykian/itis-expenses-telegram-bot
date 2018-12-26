using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

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

        public Task<IActionResult> HandleAsync(HttpListenerRequest httpRequest)
        {
            var routePath = httpRequest.Url.AbsolutePath.TrimStart('/').Split('/');
            if (routePath.Length != 2)
            {
                return Task.FromResult<IActionResult>(new BadRequestResult());
            }

            Type controllerType;
            try
            {
                controllerType = router.GetControllerType(routePath[0]);
            }
            catch (ControllerNotFoundException)
            {
                return Task.FromResult<IActionResult>(new NotFoundResult());
            }

            MethodInfo controllerAction;
            try
            {
                controllerAction = router
                    .GetControllerAction(controllerType, routePath[1], httpRequest.HttpMethod);
            }
            catch (ControllerActionNotFoundException)
            {
                return Task.FromResult<IActionResult>(new NotFoundResult());
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
                return Task.FromResult<IActionResult>(new BadRequestResult());
            }

            var result = controllerAction
                .Invoke(controllerInstance, actionParams);

            return Task.FromResult(result as IActionResult);
        }
    }
}