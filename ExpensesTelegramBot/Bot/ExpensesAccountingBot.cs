using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;

namespace Bot
{
    public class ExpensesAccountingBot : IBot
    {
        private readonly DialogSet dialogs;

        public ExpensesAccountingBot(BotAccessors botAccessors, IRequestSender requestSender)
        {
            if (requestSender == null) throw new ArgumentNullException(nameof(requestSender));

            BotAccessors = botAccessors ?? throw new ArgumentNullException(nameof(botAccessors));
            var dialogState = botAccessors.DialogStateAccessor;
            dialogs = new DialogSet(dialogState);

            dialogs.Add(new InitialDialog());
            dialogs.Add(new ChoicePrompt("initialPrompt"));

            dialogs.Add(new AuthenticationDialog(requestSender));
            dialogs.Add(new TextPrompt("secretLogin"));
            dialogs.Add(new RegistrationDialog(requestSender));
            dialogs.Add(new TextPrompt("newSecretLogin"));
            dialogs.Add(CreateCategoriesListDialog.Instance);

            dialogs.Add(new MainFunctioningDialog(requestSender));
            dialogs.Add(new ChoicePrompt("categoriesPrompt"));
            dialogs.Add(new TextPrompt("amountPrompt"));

            dialogs.Add(GetStatisticsDialog.Instance);
            dialogs.Add(ManageCategoriesDialog.Instance);
            
            dialogs.Add(new ConfirmPrompt("confirm"));
        }

        public BotAccessors BotAccessors { get; }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var dialogContext = await dialogs.CreateContextAsync(turnContext, cancellationToken);

                if (dialogContext.ActiveDialog == null )
                {
                    await dialogContext.BeginDialogAsync(InitialDialog.Id, cancellationToken);
                }
                else
                {
                    await dialogContext.ContinueDialogAsync(cancellationToken);
                }

                await BotAccessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            }
            else
            {
                await turnContext.SendActivityAsync("NO NO NO");
            }
        }
    }
}
