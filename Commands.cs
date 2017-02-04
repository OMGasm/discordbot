using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
namespace Bot
{
    class Commands
    {
        private static CommandService commands;
        private static DependencyMap map;

        public static async Task InstallCommands()
        {
            map = new DependencyMap();
            map.Add(Bot.Client);
            commands = new CommandService();
            Bot.Client.MessageReceived += HandleCommand;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public static async Task HandleCommand(SocketMessage smsg)
        {
            var msg = smsg as SocketUserMessage;
            int pos = 0;
            if (msg == null)
                return;
            if (!(msg.HasStringPrefix(@"\.", ref pos) || msg.HasMentionPrefix(Bot.Client.CurrentUser, ref pos)))
                return;
            var context = new CommandContext(Bot.Client, msg);
            var result = await commands.ExecuteAsync(context, pos, map);
            if (!result.IsSuccess)
                if(result.Error == CommandError.Exception)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}
