using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Org.BouncyCastle.Bcpg;

namespace discordbot.commands
{
    class PGP : CommandBase
    {
        public override async Task action(CommandEventArgs e)
        {
            e?.Args.First();
        }

    }
}
