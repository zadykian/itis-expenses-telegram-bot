using System;
using System.Collections.Generic;

namespace Core
{
    public class StatisticResult : ValueObject
    {
        public StatisticResult(
            IEnumerable<string> categories, 
            DateTime startDate, 
            DateTime endDate)
        {
            Categories = categories;
            StartDate = startDate;
            EndDate = endDate;
        }

        public IEnumerable<string> Categories { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}