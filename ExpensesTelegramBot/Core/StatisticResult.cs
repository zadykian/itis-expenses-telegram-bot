using System;

namespace Core
{
    public class StatisticResult : ValueObject
    {
        public readonly DateTime StartDate;
        public readonly DateTime EndDate;

        public StatisticResult(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}