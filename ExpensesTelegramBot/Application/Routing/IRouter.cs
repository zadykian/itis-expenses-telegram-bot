using System;
using System.Net;
using System.Reflection;

namespace Application
{
    public interface IRouter
    {
        Type GetControllerType(HttpListenerRequest httpRequest);

        MethodInfo GetControllerAction(HttpListenerRequest httpRequest, Type controllerType);
    }
}
