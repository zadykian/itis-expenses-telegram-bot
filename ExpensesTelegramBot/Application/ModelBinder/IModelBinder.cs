using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Reflection;

namespace Application
{
    public interface IModelBinder
    {
        object[] BindArguments(HttpListenerRequest httpRequest, MethodInfo controllerAction);
    }
}