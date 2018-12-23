using System;
using System.Collections.Generic;
using System.Text;

namespace MvcWebLibrary
{
    public interface IHttpRequestListener
    {
        //void UseConfiguration(IConfiguration configuration);
        void StartListening();
    }
}