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
        public async Task action(IGuildUser user = null)
        {
            if (user == null)
            {
                IUser owneru = (await Context.Client.GetApplicationInfoAsync()).Owner;
                IGuildUser ownergu = await Context.Channel.GetUserAsync(owneru.Id) as IGuildUser;
                string owner = ownergu?.Nickname ?? ownergu.Username ?? owneru.Username;
                ISelfUser bot = Context.Client.CurrentUser;
                double ram = GC.GetTotalMemory(false) / (1024.0 * 1024.0); //never not ram
                EmbedBuilder eb = new EmbedBuilder()
                    .WithAuthor(new EmbedAuthorBuilder()
                        .WithIconUrl(bot.AvatarUrl)
                        .WithName($"{bot.Username} 1.33.7b")
                        .WithUrl("https://github.com/OMGasm/discordbot"))
                    .WithUrl("https://github.com/OMGasm/discordbot")
                    .WithDescription($"Made by [OMGasm/Jiel](https://github.com/OMGasm/discordbot)\n\n" +
                        $"Currently operated by {owneru.Mention}\n\n" +
                        $"Eating {Math.Ceiling(ram*256)} RAM chips ({ram:0.00}MB)")
                    .WithColor(new Color(7, 129, 199))
                    .WithThumbnailUrl(owneru.AvatarUrl)
                    .WithFooter(new EmbedFooterBuilder()
                        .WithIconUrl("https://ptb.discordapp.com/assets/dcbf6274f0ce0f393d064a72db2c8913.svg")
                        .WithText("Made with RogueException/Discord.Net and ♥"));
                await ReplyAsync("", embed: eb.Build());
            }
            else
            {
                await ReplyAsync($"```text\n{(user.Nickname ?? user.Username).Replace("`", "\\`")} ({user.Id})\nJoined: {user.JoinedAt}```{user.AvatarUrl}");
            }
        }

        [Command("info")]
        public async Task action(string s)
        {
            if (s.Equals("me"))
            {
                await ReplyAsync($"```text\n{((Context.User as IGuildUser).Nickname ?? Context.User.Username).Replace("`", "\\`")} ({Context.User.Id})\nJoined: {(Context.User as IGuildUser).JoinedAt}```{Context.User.AvatarUrl}");
            }
        }
    }
}
