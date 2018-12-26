using System;
using System.Reflection;
using System.Linq;

namespace MvcWebLibrary
{
    internal class Router : IRouter
    {
        private readonly Assembly controllersAssembly;

        public Router(Assembly controllersAssembly)
        {
            this.controllersAssembly = controllersAssembly;
        }

        public Type GetControllerType(string controllerTypeName)
        {
            var specifiedControllerTypeName = $"{controllersAssembly.GetName().Name}.{controllerTypeName}Controller";
            var controllerType = controllersAssembly.GetType(specifiedControllerTypeName);
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

        private bool MethodMatches(MethodInfo controllerActionInfo, string controllerActionName, string httpMethod)
        {
            var queryContainsMethod = controllerActionInfo.Name.Equals(
                controllerActionName, 
                StringComparison.InvariantCultureIgnoreCase);

            var methodHasHttpAttribute = controllerActionInfo
                .GetCustomAttributes()
                .Any(a => IsRequiredHttpAttribute(a, httpMethod));

            return queryContainsMethod && methodHasHttpAttribute;
        }

        private bool IsRequiredHttpAttribute(Attribute attribute, string httpMethod)
            => attribute.GetType().Name
                .Contains(httpMethod, StringComparison.InvariantCultureIgnoreCase);
    }
}