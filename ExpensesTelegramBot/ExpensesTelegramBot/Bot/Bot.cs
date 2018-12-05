using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExpensesBotApi
{
    public class Bot : IBot
    {
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                var input = turnContext.Activity.Text;
                await turnContext.SendActivityAsync($"You said \"{input}\".");
            }
        }
    }
}