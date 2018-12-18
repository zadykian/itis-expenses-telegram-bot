using System;
using System.Net;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace MvcWebLibrary
{
    internal class ModelBinder : IModelBinder
    {
        public object[] BindArguments(HttpListenerRequest httpRequest, MethodInfo controllerAction)
        {
            var result = new List<object>();
            var parameterInfos = controllerAction.GetParameters();

            var collectionToBindFrom = httpRequest.HttpMethod == "GET" ?
                httpRequest.QueryString :
                httpRequest.Headers;

            foreach (var parameterInfo in parameterInfos)
            {
                var param = BindArgument(
                    parameterInfo.ParameterType, parameterInfo.Name, collectionToBindFrom);
                result.Add(param);
            }

            return result.ToArray();
        }

        private object BindArgument(
            Type parameterType, string parameterName, NameValueCollection httpRequestTokens)
        {
            if (parameterType == typeof(string))
            {
                return httpRequestTokens[parameterName];
            }
            if (parameterType.IsPrimitive)
            {
                var stringValue = httpRequestTokens[parameterName];
                return Convert.ChangeType(stringValue, parameterType);
            }
            return BindToComplexObject(parameterType, httpRequestTokens);
        }

        private object BindToComplexObject(Type parameterType, NameValueCollection httpRequestTokens)
        {
            var constructorInfo = parameterType
                .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(constructor => constructor.GetParameters().Length == 0);

            var newInstance = constructorInfo.Invoke(new object[] { });

            var properties = parameterType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                .Where(prop => !prop.Name.Contains("Id"));

            foreach (var property in properties)
            {
                var propValue = BindArgument(property.PropertyType, property.Name, httpRequestTokens);
                property.SetValue(newInstance, propValue);
            }

            return newInstance;
        }
    }
}