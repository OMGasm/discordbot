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
            string[] fn = e.GetArg("font").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            float fsize;
            if(fn.Length > 1)
            {
                fsize = float.Parse(fn[1]);
                fsize = fsize > 0 ? fsize : 11f;
                if(fn[0].Length > 1)
                {
                    Draw.setFont(fn[0], fsize);
                }
            }
            else
            {
                if (fn[0].Length > 1)
                {
                    Draw.setFont(fn[0], 11f);
                }
            }
            await Task.Yield();
        }

        public override bool permission(Command command, User user, Channel channel)
        {
            return user.Id == 121183247022555137;
        }

        public ChangeFont() : base("chfnt", parameters: new KeyValuePair<string, ParameterType>[] { new KeyValuePair<string, ParameterType>("font", ParameterType.Unparsed) }) { }
    }
}
