using System;
using System.Reflection;

namespace MvcWebLibrary
{
    public interface IRouter
    {
        Type GetControllerType(string controllerTypeName);

        MethodInfo GetControllerAction(Type controllerType, string controllerActionName, string httpMethod);
    }
}