using System;
using System.Collections.Generic;

namespace Application
{
    public class StatisticRequest
    {
        private StatisticRequest()
        {
        }

        public StatisticRequest(List<string> categories, DateTime startDate, DateTime endDate)
        {
            Categories = categories;
            Categories.ForEach(str => str.ToLowerInvariant());
            StartDate = startDate;
            EndDate = endDate;
        }

        public List<string> Categories { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}
