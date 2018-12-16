using System;
using System.Reflection;
using System.Linq;
using System.Net;

namespace Application
{
    public class Router : IRouter
    {
        public Type GetControllerType(string controllerTypeName)
        {
            var controllerType = Type.GetType($"Application.{controllerTypeName}");
            if (controllerType == null)
            {
                throw new ControllerNotFoundException(
                    $"Controller class '{controllerTypeName}' does not exist.");
            }
            return controllerType;
        }

        public MethodInfo GetControllerAction(Type controllerType, string controllerActionName, string httpMethod)
        {
            var methodInfo = controllerType
                .GetMethods()
                .FirstOrDefault(method => MethodMatches(method, controllerActionName, httpMethod));
            if (methodInfo == null)
            {
                throw new ControllerActionNotFoundException(
                    $"Controller '{controllerType.Name}' does not have method " +
                    $"'{controllerActionName}' with attribute '{httpMethod}'.");
            }        
            return methodInfo;
        }

        private static bool MethodMatches(MethodInfo controllerActionInfo, string controllerActionName, string httpMethod)
        {
            var queryContainsMethod = controllerActionInfo.Name.Equals(
                controllerActionName, 
                StringComparison.InvariantCultureIgnoreCase);

            var methodHasHttpAttribute = controllerActionInfo
                .GetCustomAttributes()
                .Any(a => IsRequiredHttpAttribute(a, httpMethod));

            return queryContainsMethod && methodHasHttpAttribute;
        }

        private static bool IsRequiredHttpAttribute(Attribute attribute, string httpMethod)
            => attribute.GetType().Name
                .Contains(httpMethod, StringComparison.InvariantCultureIgnoreCase);
    }
}