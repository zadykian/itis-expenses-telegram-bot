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
            var keyValue = httpRequestTokens[parameterName];
            if (keyValue == null)
            {
                throw new ModelBindingException(
                    $"Request does not contain required parameter '{parameterName}'.");
            }
            if (parameterType == typeof(string))
            {
                return keyValue;
            }
            if (parameterType.IsPrimitive)
            {
                return Convert.ChangeType(keyValue, parameterType);
            }
            return JsonConvert.DeserializeObject(keyValue, parameterType);
        }
    }
}