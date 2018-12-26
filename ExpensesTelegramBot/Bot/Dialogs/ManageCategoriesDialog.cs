using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class ManageCategoriesDialog : WaterfallDialog
    {
        public ManageCategoriesDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {

        }

        public static new string Id => "manageCategoriesDialog";

        public static MainFunctioningDialog Instance => new MainFunctioningDialog(Id);
    }
}
