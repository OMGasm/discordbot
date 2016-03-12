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
        public override async Task action(CommandEventArgs e)
        {
            string name;
            name = (e.GetArg("name") != "" ? e.GetArg("name") : e.User.Name);
            SHA256Managed m = new SHA256Managed();
            byte[] hash = m.ComputeHash(Encoding.UTF8.GetBytes(name));
            string s = "0";
            foreach (byte b in hash) { s += b.ToString("x2"); }
            BigInteger bigint = BigInteger.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier);
            float sexy = (float)(bigint % 1001) / 10.0f;
            await e.Channel.SendMessage(string.Format("{0} is {1:0.0}% sexy.{2}", name, sexy, ((sexy >= 80) ? " Dayumn." : "")));
        }

        public Sexymeter() : base("sexymeter",
                "Measures sexyness.",
                null, new KeyValuePair<string, ParameterType>[]
                    { new KeyValuePair<string, ParameterType>("name", ParameterType.Unparsed) })
        { }
    }
}
