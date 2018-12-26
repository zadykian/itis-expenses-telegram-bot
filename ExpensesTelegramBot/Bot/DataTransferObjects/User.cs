using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class User
    {
        private User()
        {
        }

        public User(string secretLogin)
        {
            SecretLogin = secretLogin.ToLowerInvariant();
        }

        public string SecretLogin { get; private set; }
    }
}
