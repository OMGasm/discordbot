using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace discordbot.commands
{
    class ChangeFont : CommandBase
    {
        public override async Task action(CommandEventArgs e)
        {
            Console.WriteLine(e.GetArg("font"));
            if(e.GetArg("font").Length > 1)
            Draw.setFont(e.GetArg("font"));
            await Task.Yield();
        }

        public override bool permission(Command command, User user, Channel channel)
        {
            Console.WriteLine($"{user.Id == 121183247022555137}");
            return user.Id == 121183247022555137;
        }

        public ChangeFont() : base("chfnt", parameters: new KeyValuePair<string, ParameterType>[] { new KeyValuePair<string, ParameterType>("font", ParameterType.Unparsed) }) { }
    }
}
