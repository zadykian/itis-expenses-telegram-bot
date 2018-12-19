using System;

namespace MvcWebLibrary
{
    public interface ICompositionRoot
    {
        IServiceConfigurator ServiceConfigurator { get; }

        IHttpRequestHandler GetHttpRequestHandler();

        ControllerBase GetControllerInstance(Type controllerType);
    }
}