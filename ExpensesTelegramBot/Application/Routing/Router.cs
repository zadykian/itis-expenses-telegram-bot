using System;
using System.Reflection;
using System.Linq;
using System.Net;

namespace Application
{
    public class Router : IRouter
    {
        public Type GetControllerType(HttpListenerRequest request)
        {
            var routePath = request.QueryString;
            if (routePath.Count != 2)
            {
                throw new WebException("Query is invalid.");
            }
            Type controllerType;
            try
            {
                controllerType = Type.GetType($"Application.{routePath[0]}");
            }
            catch (TargetInvocationException e)
            {
                throw new WebException(
                    $"Controller with name '{routePath[0]}' does not exists.", e);
            }
            return controllerType;
        }

        public MethodInfo GetControllerAction(HttpListenerRequest request, Type controllerType)
        {
            var routePath = request.QueryString;
            if (routePath.Count != 2)
            {
                throw new WebException("Query is invalid.");
            }
            var methodInfo = controllerType
                .GetMethods()
                .FirstOrDefault(method => MethodMatches(method, request));
            if (methodInfo != null)
            {
                throw new WebException(
                    $"Controller '{controllerType.Name}' does not have method '{routePath[1]}'.");
            }        
            return methodInfo;
        }

        private static bool MethodMatches(MethodInfo methodInfo, HttpListenerRequest request)
        {
            var queryContainsMethod = methodInfo.Name.Equals(
                request.QueryString[1], 
                StringComparison.InvariantCultureIgnoreCase);

            var methodHasHttpAttribute = methodInfo
                .GetCustomAttributes()
                .Any(a => IsRequiredHttpAttribute(a, request.HttpMethod));

            return queryContainsMethod && methodHasHttpAttribute;
        }

        private static bool IsRequiredHttpAttribute(Attribute attribute, string httpMethod)
            => attribute.GetType().Name
                .Contains(httpMethod, StringComparison.InvariantCultureIgnoreCase);
    }
}