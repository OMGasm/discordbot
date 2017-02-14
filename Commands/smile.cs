using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Bot.Commands
{
    public class smile : ModuleBase
    {
        [Command(":)")]
        [RequireOwner]
        public async Task Smile([Remainder]string s)
        {
            await Context.Channel.SendMessageAsync(s);
        }
    }
}
