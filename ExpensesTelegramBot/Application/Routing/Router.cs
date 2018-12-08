using System;
using System.Reflection;
using System.Linq;
using System.Net;

namespace Application
{
    public static class Router
    {
        public static Type GetControllerType(HttpListenerRequest request)
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

        public static MethodInfo GetControllerMethod(HttpListenerRequest request, Type controllerType)
        {
            var routePath = request.QueryString;
            if (routePath.Count != 2)
            {
                throw new WebException("Query is invalid.");
            }
            MethodInfo methodInfo;
            try
            {
                methodInfo = controllerType
                    .GetMethods()
                    .FirstOrDefault(method => MethodMatches(method, request));
            }
            catch
            {
                throw new WebException($"Controller {controllerType.Name} does not have method");
            }
            return methodInfo;
        }

        private static bool MethodMatches(MethodInfo methodInfo, HttpListenerRequest request)
            => methodInfo.Name == request.QueryString[1] &&
               methodInfo
                .GetCustomAttributes()
                .Any(a => IsHttpAttribute(a, request.HttpMethod));

        private static bool IsHttpAttribute(Attribute attribute, string httpMethod)
            => attribute.GetType().Name
                .Contains(httpMethod, StringComparison.InvariantCultureIgnoreCase);
    }
}