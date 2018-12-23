using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class StatisticResult : ValueObject
    {
        public StatisticResult(
            Dictionary<string, int> categoriesToAmounts, 
            DateTime startDate, 
            DateTime endDate)
        {
            CategoriesToAmounts = categoriesToAmounts;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Dictionary<string, int> CategoriesToAmounts { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}