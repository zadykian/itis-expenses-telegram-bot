using System;
using System.Collections.Generic;
using System.Text;

namespace MvcWebLibrary
{
    public interface IServiceConfigurator
    {
        void AddParentScopedServiceWithConstructorArg<TService, TArgument>(TArgument argument);    
    }
}
