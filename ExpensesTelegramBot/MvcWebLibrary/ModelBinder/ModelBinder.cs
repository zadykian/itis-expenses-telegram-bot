using System;
using System.Net;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Newtonsoft.Json;

namespace MvcWebLibrary
{
    internal class ModelBinder : IModelBinder
    {
        public object[] BindArguments(HttpListenerRequest httpRequest, MethodInfo controllerAction)
        {
            var parameterTypes = controllerAction
                .GetParameters()
                .Select(paramInfo => paramInfo.ParameterType);
            var result = new List<object>();
            foreach (var parameterType in parameterTypes)
            {
                var actionParam = httpRequest.HttpMethod == "GET"
                    ? BindFromQueryString(parameterType, httpRequest.QueryString)
                    : BindFromHeaders(parameterType, httpRequest.Headers);
                result.Add(actionParam);
            }
            return result.ToArray();
        }

        private static object BindFromHeaders(Type parameterType, NameValueCollection headers)
        {
            var json = headers[parameterType.Name];
            if (json == null)
            {
                throw new ModelBindingException(
                    $"Request does not contain required header '{parameterType.Name}'.");
            }
            return JsonConvert.DeserializeObject(json, parameterType);       
        }

        private static object BindFromQueryString(Type parameterType, NameValueCollection httpRequestTokens)
        {
            var keyValue = httpRequestTokens[parameterType.Name];
            if (parameterType == typeof(string) || parameterType.IsPrimitive)
            {
                if (keyValue == null)
                {
                    throw new ModelBindingException(
                        $"Request does not contain required parameter '{parameterType.Name}' in query string.");
                }
                return Convert.ChangeType(keyValue, parameterType);
            }
            return BindToComplexObject(parameterType, httpRequestTokens);
        }

        private static object BindToComplexObject(Type parameterType, NameValueCollection httpRequestTokens)
        {
            var newInstance = Activator.CreateInstance(parameterType);
            var properties = parameterType.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.SetProperty);
            foreach (var property in properties)
            {
                var propValue = BindFromQueryString(property.PropertyType, httpRequestTokens);
                property.SetValue(newInstance, propValue);
            }
            return newInstance;
        }
    }
}