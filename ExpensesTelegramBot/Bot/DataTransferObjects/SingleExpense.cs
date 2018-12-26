using System;

namespace Bot
{
    public class SingleExpense
    {
        private SingleExpense()
        {
        }

        public SingleExpense(Channel channel, DateTime creationDateTime, string category, int amount)
        {
            Id = Guid.NewGuid();
            Channel = channel;
            CreationDateTime = creationDateTime;
            Category = category.ToLowerInvariant();
            Amount = amount;
        }

        public Guid Id { get; private set; }

        public string ChannelId { get; private set; }

        public Channel Channel { get; private set; }

        public DateTime CreationDateTime { get; private set; }

        public string Category { get; private set; }

        public int Amount { get; private set; }
    }
}