using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class CreateCategoriesListDialog : WaterfallDialog
    {
        public CreateCategoriesListDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {

        }

        public static new string Id => "createCategoriesListDialog";

        public static CreateCategoriesListDialog Instance => new CreateCategoriesListDialog(Id);
    }
}
