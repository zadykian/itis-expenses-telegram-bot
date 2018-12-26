using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bot
{
    public class AuthenticationDialog : WaterfallDialog
    {
        private readonly IRequestSender requestSender;

        public AuthenticationDialog(IRequestSender requestSender)
            : this(Id)
        {
            this.requestSender = requestSender;
        }

        public AuthenticationDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                var promptOptions = new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please enter your SecretLogin.")
                };
                return await stepContext.PromptAsync("secretLogin", promptOptions, cancellationToken);
            });

            AddStep(async (stepContext, cancellationToken) => 
            {
                var login = stepContext.Result as string;
                var user = new User(login);
                if (await requestSender.CheckIfUserExists(user))
                {
                    await stepContext.Context
                        .SendActivityAsync(MessageFactory.Text("Welcome back!"), cancellationToken);

                    

                    return await stepContext.BeginDialogAsync(MainFunctioningDialog.Id);
                }
                else
                {
                    await stepContext.Context
                        .SendActivityAsync(MessageFactory.Text("This user does not exists."), cancellationToken);
                    return await stepContext.BeginDialogAsync(InitialDialog.Id);
                }
            });
        }

        public static new string Id => "authenticationDialog";
    }
}
