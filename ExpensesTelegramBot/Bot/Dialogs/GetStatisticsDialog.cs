using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class GetStatisticsDialog : WaterfallDialog
    {
        public GetStatisticsDialog(string dialogId, IEnumerable<WaterfallStep> steps = null)
            : base(dialogId, steps)
        {

        }

        public static new string Id => "getStatisticsDialog";

        public static GetStatisticsDialog Instance => new GetStatisticsDialog(Id);
    }
}
