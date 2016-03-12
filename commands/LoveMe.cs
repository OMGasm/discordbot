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
            await e.Channel.SendMessage($"I love you {e.User.Name}! ♥");
        }

        public LoveMe() : base("love me plz", "make makotoe love you") { }
    }
}
