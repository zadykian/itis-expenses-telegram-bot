using System;

namespace MvcWebLibrary
{
    public interface ICompositionRoot
    {
        IRequestHandler GetRequestHandler();

        ControllerBase GetControllerInstance(Type controllerType);
    }
}