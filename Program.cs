using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Modules;

namespace discordbot
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.CancelKeyPress += (s, e) => Environment.Exit(0);
            Console.OutputEncoding = Encoding.Unicode;
            DiscordClient client = new DiscordClient();
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
                if (!client.Servers.Any())
                {
                    Console.WriteLine("No available servers.");
                    return;
                }
                client.UsingCommands(conf =>
                {
                    conf.AllowMentionPrefix = true;
                    conf.PrefixChar = null;
                });
                Commands.init(client.Services.Get<CommandService>());
            });
        }
    }
}
