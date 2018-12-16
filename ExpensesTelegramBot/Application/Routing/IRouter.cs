using System;
using System.Reflection;

namespace Application
{
    public interface IRouter
    {
        Type GetControllerType(string controllerTypeName);

        MethodInfo GetControllerAction(Type controllerType, string controllerActionName, string httpMethod);
    }
}