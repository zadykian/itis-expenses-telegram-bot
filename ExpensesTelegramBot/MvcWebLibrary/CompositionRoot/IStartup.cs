using System;
using System.Collections.Generic;
using System.Text;

namespace MvcWebLibrary
{
    public interface IStartup
    {
        void ConfigureServices(ICompositionRoot compositionRoot);
    }
}