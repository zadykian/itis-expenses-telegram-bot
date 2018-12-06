using Telegram.Bot;
using MihaZupan;
using Telegram.Bot.Args;
using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace TelegramClient
{
    public class TelegramClient
    {
        private readonly ITelegramBotClient client;

        public TelegramClient()
        {
            client = new TelegramBotClient(AppSettings.ApiToken, Proxy.GetProxyIfNessesary());
            client.OnMessage += ActOnMessage;
        }

        public static TelegramClient RunNew()
        {
            Console.WriteLine("Bot is waiting for messages...");
            var newBot = new TelegramClient();
            newBot.client.StartReceiving();
            return newBot;
        }

        public void Run()
        {
            Console.WriteLine("Bot is waiting for messages...");
            client.StartReceiving();
        }

        private async void ActOnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}");
                var result = $"You said {e.Message.Text}";
                await client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: result);
            }
        }
    }
}