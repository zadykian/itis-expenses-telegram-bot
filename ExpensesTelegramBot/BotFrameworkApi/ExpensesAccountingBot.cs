﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace BotFrameworkApi
{
    public class ExpensesAccountingBot : IBot
    {                       
        public async Task OnTurnAsync(ITurnContext turnContext, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {   
                await turnContext.SendActivityAsync("Hello World", cancellationToken: cancellationToken);
            }
        }
    }
}
