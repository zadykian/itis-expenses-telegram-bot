using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Application
{
    public class StatisticResult
    {
        public StatisticResult(
            string channelId,
            Dictionary<string, int> categoriesToAmounts, 
            DateTime startDateTime, 
            DateTime endDateTime)
        {
            ChannelId = channelId;
            CategoriesToAmounts = categoriesToAmounts;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        public string ChannelId { get; private set; }
        public Dictionary<string, int> CategoriesToAmounts { get; set; }
        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }
    }
}