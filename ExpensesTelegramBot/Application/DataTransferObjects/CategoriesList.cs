using System.Collections.Generic;

namespace Application
{
    public class CategoriesList
    {
        public CategoriesList(string channelId, List<string> categories)
        {
            ChannelId = channelId;
            Categories = categories;
        }

        public string ChannelId { get; private set; }

        public List<string> Categories { get; private set; }
    }
}
