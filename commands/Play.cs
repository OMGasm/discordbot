using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace discordbot.commands
{
    class Play : CommandBase
    {
        string game;

        public override async Task action(CommandEventArgs e)
        {
            Console.WriteLine(e.User.Id);
            if(e.User.Id == 121183247022555137)
            {
                game = e.GetArg("game");
                e.Channel.Client.SetGame(game);
                e.Channel.Client.GatewaySocket.Connected += (s,ev) => e.Channel.Client.SetGame(game);
            }
            await Task.Yield();
        }

        public Play() : base("play", "play a game", null, new KeyValuePair<string, ParameterType>[] { new KeyValuePair<string, ParameterType>("game", ParameterType.Unparsed) }) { }
    }
}
