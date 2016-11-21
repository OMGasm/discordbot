using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Modules;
using Discord.Audio;
namespace discordbot
{
    class Program
    {
        private static DiscordClient _client = new DiscordClient();
        internal static DiscordClient client => _client;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += (s, e) => { client.Disconnect(); Environment.Exit(0); };
            Console.OutputEncoding = Encoding.Unicode;
            client.ExecuteAndWait(async () =>
            {
                if (Auth.loadToken())
                {
                    Console.WriteLine("Logging in...");
                    await client.Connect(Auth.token);
                }
                else
                {
                    Auth.token = await client.Connect(Auth.email, Auth.password);
                    Auth.saveToken();
                }
                Console.Clear();
                client.Log.Message += async (s, e) => await Logger.log(e);
                client.MessageReceived += async (s, e) => await Logger.log(e);
                client.UsingCommands(conf =>
                {
                    conf.AllowMentionPrefix = true;
                    conf.CustomPrefixHandler = (Message m) => m.RawText.StartsWith("\\.") && !m.RawText.StartsWith("\\. ") ? 2 : -1;
                });
                Commands.init(client.GetService<CommandService>());
                client.UsingAudio(x => x.Mode = AudioMode.Outgoing);
            });
        }
    }
}
