using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public interface IRequestSender
    {
        Task<bool> CheckIfUserExists(User user);

        Task<bool> CreateNewUserIfNotExists(User user, Channel channel);

        void RegisterChannelIfNotExists(Channel channel);
    }
}
