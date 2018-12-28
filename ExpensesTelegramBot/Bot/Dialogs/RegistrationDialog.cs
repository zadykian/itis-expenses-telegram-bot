using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class RegistrationDialog : WaterfallDialog
    {
        private readonly IRequestSender requestSender;

        public RegistrationDialog(IRequestSender requestSender)
            : this(Id)
        {
            this.requestSender = requestSender;
        }

        public RegistrationDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                var promptOptions = new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please enter new SecretLogin (from 8 to 32 characters).")
                };
                return await stepContext.PromptAsync("newSecretLogin", promptOptions, cancellationToken);
            });

            AddStep(async (stepContext, cancellationToken) =>
            {

                if (!(stepContext.Result is string login) || login.Length < 8 || login.Length > 32)
                {
                    await stepContext.Context
                        .SendActivityAsync(MessageFactory.Text("Invalid login format! Try again."), cancellationToken);
                    return await stepContext.BeginDialogAsync(Id);
                }
                var user = new User(login);
                var channel = new Channel(user, stepContext.Context.Activity.ChannelId);
                if (await requestSender.CreateNewUserIfNotExists(channel))
                {
                    await stepContext.Context
                        .SendActivityAsync(MessageFactory.Text("Welcome! Now you should configure your categories."), cancellationToken);
                    return await stepContext.BeginDialogAsync(CreateCategoriesListDialog.Id);
                }
                else
                {
                    await stepContext.Context
                        .SendActivityAsync(MessageFactory.Text("User with this login already exists."), cancellationToken);
                    return await stepContext.BeginDialogAsync(InitialDialog.Id);
                }
            });
        }

        public static new string Id => "registrationDialog";
    }
}
