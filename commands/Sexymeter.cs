using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;
using Discord;
using Discord.Commands;

namespace Bot.commands
{
    public class Sexymeter : ModuleBase
    {
        public static float calculate(string name)
        {
            SHA256Managed m = new SHA256Managed();
            byte[] hash = m.ComputeHash(Encoding.UTF8.GetBytes(name));
            string num = "0";
            foreach (byte b in hash) { num += b.ToString("x2"); }
            BigInteger bigint = BigInteger.Parse(num, System.Globalization.NumberStyles.AllowHexSpecifier);
            return (float)(bigint % 1001) / 10.0f;
        }

        [Command("sexymeter"), Summary("Check how sexy you are")]
        public async Task action([Remainder] string name = "")
        {
            ulong id;
            string nick = null;
            IEnumerable<IGuildUser> users = null;
            if (name == "")
            {
                name = Context.User.Username;
                nick = (Context.User as IGuildUser)?.Nickname ?? name;
            }
            else if (MentionUtils.TryParseUser(name, out id))
            {
                var user = await Context.Channel.GetUserAsync(id);
                name = user.Username;
                nick = (user as IGuildUser)?.Nickname ?? name;
            }
            else if ((users = (await Context.Guild.GetUsersAsync())
                .Where(x => x.Username == name || x.Nickname == name)).Any())
            {
                if (users.Any(x => x.Id == Context.User.Id))
                {
                    name = Context.User.Username;
                    nick = (Context.User as IGuildUser)?.Nickname ?? name;
                }
                else if (users.Any(x => x.Status == UserStatus.Online))
                {
                    //name 

                }
            }
            /*else if(MentionUtils.TryParseRole(name, out id))
            {
                name = Context.Guild.GetRole(id).Name;
            }
            else if(MentionUtils.TryParseChannel(name, out id))
            {
                name = (await Context.Guild.GetChannelAsync(id)).Name;
            }*/
            //name = name != "" ? name : Context.User.Username;
            float sexyness = calculate(name);
            await ReplyAsync($"{name} is {sexyness:0.0}% sexy{(sexyness >= 80 ? ", Dayumn." : "")}");
        }
    }
}
