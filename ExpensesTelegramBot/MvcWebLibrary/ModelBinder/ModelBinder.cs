using System;
using System.Net;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;

namespace MvcWebLibrary
{
    internal class ModelBinder : IModelBinder
    {
        public object[] BindArguments(HttpListenerRequest httpRequest, MethodInfo controllerAction)
        {
            var parameterInfos = controllerAction.GetParameters()
                .Where(paramInfo => !paramInfo.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            var result = new List<object>();

            var collectionToBindFrom = httpRequest.HttpMethod == "GET"
                ? httpRequest.QueryString
                : httpRequest.Headers;

            foreach (var parameterInfo in parameterInfos)
            {
                var actionParam = BindFromJson(parameterInfo, collectionToBindFrom);
                result.Add(actionParam);
            }
            return result.ToArray();
        }

        private object BindFromJson(ParameterInfo parameterInfo, NameValueCollection httpTokens)
        {
            var json = httpTokens[parameterInfo.Name];
            if (json == null)
            {
                throw new ModelBindingException(
                    $"Request does not contain required header '{parameterInfo.Name}'.");
            }
            return JsonConvert.DeserializeObject(json, parameterInfo.ParameterType);
        }
    }
}