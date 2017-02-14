using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
namespace Bot
{
    class Bot
    {
        private DiscordSocketClient _client;
        private static Bot p;
        
        internal static DiscordSocketClient Client => p._client;

        static void Main(string[] args) => (p = new Bot()).Run(args).GetAwaiter().GetResult();

        async Task Run(string[] args)
        {
            Console.WriteLine("Loading...");
            //Config cfg = await Config.Load("config.json");
            Config cfg = new Config() { token = System.IO.File.ReadAllText("token"), bot = true };
            AppDomain.CurrentDomain.UnhandledException += async (s, e) =>
                await Logger.Log(new LogMessage(LogSeverity.Critical, "unhandled exception", "unhandled exception", e.ExceptionObject as Exception));
            _client = new DiscordSocketClient();
            Client.MessageReceived += e => Logger.Message(e);
            Client.Log += e => Logger.Log(e);
            await CommandHandler.InstallCommands();

            Console.WriteLine("Logging in...");
            await Client.LoginAsync(TokenType.Bot, cfg.token);
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
