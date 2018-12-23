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
            var parameterInfos = controllerAction.GetParameters();
            var result = new List<object>();
            foreach (var parameterInfo in parameterInfos)
            {
                var actionParam = httpRequest.HttpMethod == "GET"
                    ? BindFromQueryString(parameterInfo.ParameterType, parameterInfo.Name, httpRequest.QueryString)
                    : BindFromHeaders(parameterInfo, httpRequest.Headers);
                result.Add(actionParam);
            }
            return result.ToArray();
        }

        private static object BindFromHeaders(ParameterInfo parameterInfo, NameValueCollection headers)
        {
            var json = headers[parameterInfo.Name];
            if (json == null)
            {
                throw new ModelBindingException(
                    $"Request does not contain required header '{parameterInfo.Name}'.");
            }
            return JsonConvert.DeserializeObject(json, parameterInfo.ParameterType);       
        }

        private static object BindFromQueryString(Type parameterType, string parameterName, NameValueCollection httpRequestTokens)
        {
            if (parameterType == typeof(string) || parameterType.IsPrimitive)
            {
                var keyValue = httpRequestTokens[parameterName];
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
            var constructorInfo = parameterType
                .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(constrInfo => constrInfo.GetParameters().Length == 0);

            if (constructorInfo == null)
            {
                throw new MissingMethodException(
                    $"Class '{parameterType.Name}' does not contain parameterless constructor.");
            }

            var newInstance = constructorInfo.Invoke(new object[] { });
            var properties = parameterType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                .Where(prop => !prop.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            
            foreach (var property in properties)
            {
                var propValue = BindFromQueryString(property.PropertyType, property.Name, httpRequestTokens);
                property.SetValue(newInstance, propValue);
            }
            return newInstance;
        }
    }
}