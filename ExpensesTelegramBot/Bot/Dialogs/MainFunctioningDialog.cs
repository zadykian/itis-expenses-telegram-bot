using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class MainFunctioningDialog : WaterfallDialog
    {
        public MainFunctioningDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {

        }

        public static new string Id => "mainFunctioningDialog";

        public static MainFunctioningDialog Instance => new MainFunctioningDialog(Id);
    }
}
