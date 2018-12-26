using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot
{
    public class StatisticResult
    {
        public StatisticResult(
            User user,
            Dictionary<string, int> categoriesToAmounts, 
            DateTime startDate, 
            DateTime endDate)
        {
            User = user;
            CategoriesToAmounts = categoriesToAmounts;
            StartDate = startDate;
            EndDate = endDate;
        }

        public User User { get; private set; }
        public Dictionary<string, int> CategoriesToAmounts { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}