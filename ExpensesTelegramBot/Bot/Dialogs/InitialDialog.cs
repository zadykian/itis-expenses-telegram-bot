using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class InitialDialog : WaterfallDialog
    {
        public InitialDialog()
            : this(Id)
        {
        }

        public InitialDialog(string dialogId, IEnumerable<WaterfallStep> steps = null) 
            : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                return await stepContext.PromptAsync("initialPrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply("Hello! I am your personal BUHGALTER"),
                        Choices = new[] { new Choice("Log in"), new Choice("Create new user") }
                    });
            });

            AddStep(async(stepContext, cancellationToken) => 
            {
                var response = (stepContext.Result as FoundChoice)?.Value;

                if (response == "Log in")
                {
                    return await stepContext.BeginDialogAsync(AuthenticationDialog.Id);
                }
                if (response == "Create new user")
                {
                    return await stepContext.BeginDialogAsync(RegistrationDialog.Id);
                }
                return await stepContext.NextAsync();
            });
        }

        public static new string Id => "initialDialog";
    }
}
