using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure
{
    public static class Configuration
    {
        public static string GetConnectionString(string connectionStringKey)
        {
            return "Server=(localdb)\\mssqllocaldb;Database=ExpensesBotDb;Trusted_Connection=True;";
            //return new ConfigurationBuilder()
            //    .SetBasePath(AppContext.BaseDirectory)
            //    .AddJsonFile("appsettings.json")
            //    .Build()
            //    .GetConnectionString(connectionStringKey);
        }
    }
}
