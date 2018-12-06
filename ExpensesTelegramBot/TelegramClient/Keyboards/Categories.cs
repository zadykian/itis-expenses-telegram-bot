using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramClient
{
    public class Categories
    {
        private readonly List<string> categories;

        public Categories(List<string> categories)
        {
            this.categories = categories;
        }

        public InlineKeyboardMarkup MakeKeyboard()
            => new InlineKeyboardMarkup(CategoriesToButtons());


        // СДЕЛАТЬ НОРМАЛЬНО!!!
        private IEnumerable<IEnumerable<InlineKeyboardButton>> CategoriesToButtons()
        {
            for (var i = 0; i < categories.Count; i += 2)
            {
                yield return new[] 
                {
                    InlineKeyboardButton.WithCallbackData(categories[i]),
                    InlineKeyboardButton.WithCallbackData(categories[i + 1])
                };
            }
            if (categories.Count % 2 == 1)
            {
                yield return new[]
                {
                    InlineKeyboardButton.WithCallbackData(categories[categories.Count - 1])
                };
            }
        }
    }
}
