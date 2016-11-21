using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.Diagnostics;
using Discord;
using System.Text.RegularExpressions;
using System.IO;
using NLua;
using NLua;
using Nito.AsyncEx;

namespace discordbot.commands
{
    class Lua : CommandBase
    {
        AsyncLock alock = new AsyncLock();

        string s;

        void message(string s)
        {
            this.s += s;
        }

        public override async Task action(CommandEventArgs e)
        {
            await Task.Delay(0);
            Regex regex = new Regex("^```lua\\n(.+?)```$", RegexOptions.Singleline);
            Match match = regex.Match(e.GetArg("code"));
            if (match.Success)
            {
                NLua.Lua state = new NLua.Lua();
                state.LoadCLRPackage();
                state["e"] = e;
                state.RegisterFunction("print", this, GetType().GetMethod("message"));
                using (var lck = await alock.LockAsync())
                {
                    s = "";
                    state.DoString(match.Value);
                    await e.Channel.SendMessage(s);
                }
            }
        }

        public override bool permission(Command command, User user, Channel channel)
        {
            return user.Id == 121183247022555137;
        }

        public Lua() : base("lua", parameters: new KeyValuePair<string, ParameterType>[] { new KeyValuePair<string, ParameterType>("code", ParameterType.Unparsed) })
        { }
    }
}
