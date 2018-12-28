using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class CreateCategoriesListDialog : WaterfallDialog
    {
        private readonly IRequestSender requestSender;

        public CreateCategoriesListDialog(IRequestSender requestSender)
            : base(Id)
        {
            this.requestSender = requestSender;
        }

        public CreateCategoriesListDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {

            AddStep(async (stepContext, cancellationToken) =>
            {
                var promptOptions = new PromptOptions
                {
                    Prompt = MessageFactory.Text("Give me the list of categories separated by a space!")
                };
                return await stepContext.PromptAsync("createCategoriesPrompt", promptOptions, cancellationToken);
            });

            AddStep(async (stepContext, cancellationToken) =>
            {
                if (!(stepContext.Result is string categories) || categories.Length == 0 || categories.Length > 1024)
                {
                    await stepContext.Context
                        .SendActivityAsync(MessageFactory.Text("Wrong input! Try again."), cancellationToken);
                    return await stepContext.BeginDialogAsync(Id);
                }

                await requestSender.SetCategoriesList(stepContext.Context.Activity.ChannelId, categories);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Done!"), cancellationToken);
                return await stepContext.BeginDialogAsync(MainFunctioningDialog.Id);
               
            });
        }

        public static new string Id => "createCategoriesListDialog";
    }
}
