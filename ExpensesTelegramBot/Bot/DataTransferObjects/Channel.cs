using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class Channel
    {
        private Channel()
        {
        }

        public Channel(string id) : this(null, id)
        {
        }

        public Channel(User user, string id)
        {
            Id = id;
            User = user;
            UserSecretLogin = user?.SecretLogin;
        }

        public string Id { get; private set; }

        public string UserSecretLogin { get; private set; }

        public User User { get; private set; }
    }
}
