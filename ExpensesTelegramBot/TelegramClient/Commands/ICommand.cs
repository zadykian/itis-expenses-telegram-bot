using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramClient.Commands
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(Message message, TelegramBotClient client);
    }
}
