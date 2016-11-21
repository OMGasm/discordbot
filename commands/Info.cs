using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace discordbot.commands
{
    class Info : CommandBase
    {
        public override async Task action(CommandEventArgs e)
        {
            if (e.GetArg("user") == "")
            {
                await e.Channel.SendMessage($"```Makotoe Discord bot version 0.3a.\n" +
                    $"Running with Discord.Net { "0.9.1" } on { Environment.OSVersion }.\n" +
                    $"Current heap usage: { GC.GetTotalMemory(false) / (1024.0 * 1024.0):0.00}MB.\n" +
                    $"Owned and operated by { /* TODO: get owner from config */ "OMGasm (121183247022555137)" }.```");
            }
            else if (e.GetArg("user").ToLower().Equals("me"))
            {
                await e.Channel.SendMessage($"```text\n{e.User.Name.Replace("`", "\\`")} ({e.User.Id})\nJoined: {e.User.JoinedAt}```{e.User.AvatarUrl}");
            }
            else if (e.Message.MentionedUsers.Count() > 0)
            {
                await e.Channel.SendMessage($"```text\n{e.Message.MentionedUsers.First().Name.Replace("`", "\\`")} ({e.Message.MentionedUsers.First().Id})\nJoined: {e.Message.MentionedUsers.First().JoinedAt}```{e.Message.MentionedUsers.First().AvatarUrl}");
            }
        }

        public Info() : base("info",
            parameters: new KeyValuePair<string, ParameterType>[] 
            { new KeyValuePair<string, ParameterType>("user", ParameterType.Optional) })
        { }
    }
}
