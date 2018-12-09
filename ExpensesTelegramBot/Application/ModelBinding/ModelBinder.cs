using System;
using System.Net;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Application
{
    public class ModelBinder : IModelBinder
    {
        public object[] BindArguments(
            HttpListenerRequest httpRequest, 
            MethodInfo controllerAction)
        {
            var result = new List<object>();
            var parameterInfos = controllerAction.GetParameters();

            var collectionToBindFrom = httpRequest.HttpMethod == "GET" ?
                httpRequest.QueryString :
                httpRequest.Headers;

            foreach (var parameterInfo in parameterInfos)
            {
                var param = BindArgument(
                    parameterInfo.ParameterType, collectionToBindFrom);
                result.Add(param);
            }

            return result.ToArray();
        }

        private object BindArgument(
            Type parameterType, 
            NameValueCollection httpRequestTokens)
        {
            if (parameterType.IsPrimitive)
            {
                var stringValue = httpRequestTokens[parameterType.Name];
                return Convert.ChangeType(stringValue, parameterType);
            }
            var properties = parameterType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            
        }
    }
}
