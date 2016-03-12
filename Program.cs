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
                Auth.token = await client.Connect(Auth.email, Auth.password);
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
