using MvcWebLibrary;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using System;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.CreateDefault()
                .UseStartup<Startup>()
                .Run();
        }
    }
}