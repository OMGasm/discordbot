using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;
using Discord;
using Discord.Commands;
using Discord.Commands.Permissions;

namespace discordbot.commands
{
    class Sexymeter : CommandBase
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

        public override async Task action(CommandEventArgs e)
        {
            Console.WriteLine(1);
            if (e.Message.MentionedRoles.Contains(e.Server.EveryoneRole))
            {
                await e.Channel.SendMessage($"I don't like you anymore, {e.User.Name}.");
                return;
            }
            string name = (e.GetArg("name") != "" ? e.GetArg("name") : e.User.Name);
            float sexyness = calculate(name);
            await e.Channel.SendMessage(string.Format("{0} is {1:0.0}% sexy.{2}", name, sexyness, ((sexyness >= 80) ? " Dayumn." : "")));
        }

        public Sexymeter() : base("sexymeter",
                "Measures sexyness.",
                null, new KeyValuePair<string, ParameterType>[]
                    { new KeyValuePair<string, ParameterType>("name", ParameterType.Unparsed) })
        { }
    }
}
