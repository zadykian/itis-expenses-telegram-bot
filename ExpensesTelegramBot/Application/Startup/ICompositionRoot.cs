using System;

namespace Application
{
    public interface ICompositionRoot
    {
        void ConfigureServices();

        IRequestHandler GetRequestHandler();

        ControllerBase GetControllerInstance(Type controllerType);
    }
}