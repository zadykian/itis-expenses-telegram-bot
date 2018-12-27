using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public interface IRequestSender
    {
        Task<bool> CheckIfUserExists(User user);

        Task<bool> CreateNewUserIfNotExists(Channel channel);

        Task RegisterChannelIfNotExists(Channel channel);

        Task<List<string>> GetRegularCategories(Channel channel);

        Task AddSingleExpense(SingleExpense singleExpense);
    }
}
