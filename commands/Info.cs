using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Bot.commands
{
    public class Info : ModuleBase
    {
        [Command("info"), Summary("Gets info of the bot or you")]
        public async Task action([Remainder]string user = "")
        {
            if (user == "")
            {
                await ReplyAsync($"```Makotoe Discord bot version 0.4a.\n" +
                    $"Running with Discord.Net 1.0.X on { Environment.OSVersion }.\n" +
                    $"Current heap usage: { GC.GetTotalMemory(false) / (1024.0 * 1024.0):0.00}MB.\n" +
                    $"Owned and operated by { /* TODO: get owner from config */ "OMGasm (121183247022555137)" }.```");
            }
            else if (user.Equals("me"))
            {
                await ReplyAsync($"```text\n{((Context.User as IGuildUser).Nickname ?? Context.User.Username).Replace("`", "\\`")} ({Context.User.Id})\nJoined: {(Context.User as IGuildUser).JoinedAt}```{Context.User.AvatarUrl}");
            }
            else if (Context.Message.MentionedUserIds.Count() > 0)
            {
                var mentioned = await Context.Guild.GetUserAsync(Context.Message.MentionedUserIds.First());
                await ReplyAsync($"```text\n{mentioned.Nickname?.Replace("`", "\\`")} ({mentioned.Id})\nJoined: {mentioned.JoinedAt}```{mentioned.AvatarUrl}");
            }
        }
    }
}
