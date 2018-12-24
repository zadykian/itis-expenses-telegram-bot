using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class StatisticResult
    {
        public StatisticResult(
            string userLogin,
            Dictionary<string, int> categoriesToAmounts, 
            DateTime startDate, 
            DateTime endDate)
        {
            UserLogin = userLogin;
            CategoriesToAmounts = categoriesToAmounts;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string UserLogin { get; private set; }
        public Dictionary<string, int> CategoriesToAmounts { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}