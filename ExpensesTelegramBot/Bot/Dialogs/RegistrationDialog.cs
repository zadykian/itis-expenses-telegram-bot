using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class RegistrationDialog : WaterfallDialog
    {
        public RegistrationDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {

        }

        public static new string Id => "registrationDialog";

        public static RegistrationDialog Instance => new RegistrationDialog(Id);
    }
}
