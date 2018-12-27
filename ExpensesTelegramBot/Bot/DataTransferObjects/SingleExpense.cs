using System;

namespace Bot
{
    public class SingleExpense
    {
        public SingleExpense()
        {
            Id = Guid.NewGuid();
        }

        public SingleExpense(Channel channel, DateTimeOffset? creationDateTime, string category, int amount)
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

        public DateTimeOffset? CreationDateTime { get; set; }

        public string Category { get; set; }

        public int Amount { get; set; }

        public void SetChannel(Channel channel)
        {
            if (Channel != null)
            {
                throw new InvalidOperationException("Channel cannot be assingned more than once");
            }
            Channel = channel;
            ChannelId = channel.Id;
        }
    }
}