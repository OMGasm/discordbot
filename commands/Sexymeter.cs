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

        [Priority(1)]
        [Command("sexymeter"), Summary("Check how sexy you are")]
        public async Task action(IUser user)
        {
            string sexyname = user.Username;
            string name = (user as IGuildUser)?.Nickname ?? sexyname;
            float sexyness = calculate(sexyname);
            await ReplyAsync($"{name} is {sexyness:0.0}% sexy{(sexyness >= 80 ? ", Dayumn." : "")}");
        }

        [Priority(2)]
        [Command("sexymeter"), Summary("Check how sexy you are")]
        public async Task action([Remainder] string name = "")
        {
            string sexyname = name;
            if (name == "")
            {
                sexyname = Context.Message.Author.Username;
                name = (Context.Message.Author as IGuildUser)?.Nickname ?? sexyname;
            }
            float sexyness = calculate(sexyname);
            await ReplyAsync($"{name} is {sexyness:0.0}% sexy{(sexyness >= 80 ? ", Dayumn." : "")}");
        }
    }
}
