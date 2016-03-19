using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Commands.Permissions;

namespace discordbot.commands
{
    class LoveMe : CommandBase
    {
        public override async Task action(CommandEventArgs e)
        {
            if(Sexymeter.calculate(e.User.Name) >= 80)
            {
                await e.Channel.SendMessage($"But I already have Haru.");
            }
            else
            {
                await e.Channel.SendMessage("No.");
            }
        }

        public LoveMe() : base("love me plz", "make makotoe love you") { }
    }
}
