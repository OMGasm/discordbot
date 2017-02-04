using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Bot.commands
{
    public class Play : ModuleBase
    {
        [RequireOwner]
        [Command("play"), Summary("Set playing status")]
        public async Task action(string game, string streamurl = null)
        {
            await Bot.Client.SetGameAsync(game, streamurl, streamurl == null ? StreamType.NotStreaming : StreamType.Twitch);
        }
    }
}
