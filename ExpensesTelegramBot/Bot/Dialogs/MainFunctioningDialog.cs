using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bot
{
    public class MainFunctioningDialog : WaterfallDialog
    {
        private readonly IRequestSender requestSender;

        public MainFunctioningDialog(IRequestSender requestSender)
            : base(Id)
        {
            this.requestSender = requestSender;
        }

        public MainFunctioningDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {
            var singleExpense = new SingleExpense();

            AddStep(async (stepContext, cancellationToken) =>
            {
                var channel = new Channel(stepContext.Context.Activity.ChannelId);
                singleExpense.SetChannel(channel);
                var categories = await requestSender.GetRegularCategories(channel);
                var choices = categories.Select(category => new Choice(category)).ToList();
                return await stepContext.PromptAsync("categoriesPrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply("Choose category!"),
                        Choices = choices
                    });
            });

            AddStep(async (stepContext, cancellationToken) =>
            {
                var channel = new Channel(stepContext.Context.Activity.ChannelId);
                singleExpense.SetChannel(channel);
                var categories = await requestSender.GetRegularCategories(channel);
                var choices = categories.Select(category => new Choice(category)).ToList();
                return await stepContext.PromptAsync("categoriesPrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply("Choose category!"),
                        Choices = choices
                    });
            });

            AddStep(async (stepContext, cancellationToken) =>
            {
                singleExpense.CreationDateTime = stepContext.Context.Activity.Timestamp;
                if (!(stepContext.Result is string category))
                {
                    await stepContext.Context
                        .SendActivityAsync(MessageFactory.Text("Invalid input! Try again"), cancellationToken);
                    return await stepContext.BeginDialogAsync(Id);
                }
                singleExpense.Category = category;

                var promptOptions = new PromptOptions
                {
                    Prompt = MessageFactory.Text("How much $$$?")
                };
                return await stepContext.PromptAsync("amountPrompt", promptOptions, cancellationToken);
            });

            AddStep(async (stepContext, cancellationToken) =>
            {
                singleExpense.Amount = (int)stepContext.Result;
                if (!int.TryParse(stepContext.Result.ToString(), out var amount))
                {
                    await stepContext.Context
                        .SendActivityAsync(MessageFactory.Text("Narushitel! Try again."), cancellationToken);
                    return await stepContext.BeginDialogAsync(Id);
                }
                singleExpense.Amount = amount;
                await requestSender.AddSingleExpense(singleExpense);
                await stepContext.Context
                    .SendActivityAsync(MessageFactory.Text("Ok!"), cancellationToken);
                return await stepContext.BeginDialogAsync(Id);
            });
        }

        public static new string Id => "mainFunctioningDialog";
    }
}
