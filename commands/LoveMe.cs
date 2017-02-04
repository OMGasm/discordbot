using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Bot.commands
{
    public class LoveMe : ModuleBase
    {
        [Command("love me plz"), Summary("Try to get makotoe to love you")]
        public async Task action()
        {
            if(Sexymeter.calculate(Context.User.Username) >= 80)
            {
                await ReplyAsync($"But I already have Haru.");
            }
            else
            {
                await ReplyAsync("No.");
            }
        }
    }
}
