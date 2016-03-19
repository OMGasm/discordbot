using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using discordbot.commands;
namespace discordbot
{
    class Commands
    {
        internal static CommandService service;
        static List<CommandBase> commands;

        static Commands()
        {
            commands = new List<CommandBase>();
        }

        internal static void init(CommandService _service)
        {
            service = _service;
            CefSharp.Cef.Initialize(new CefSharp.CefSettings(), shutdownOnProcessExit: true, performDependencyCheck: true);
            commands.Add(new Sexymeter());
            commands.Add(new LoveMe());
            commands.Add(new Play());
            commands.Add(new Draw());
            commands.Add(new ChangeFont());
            commands.Add(new Web());
        }

        internal static void addCommand(CommandBase command)
        {
            commands.Add(command);
        }

        internal static void load(string cmd)
        {
            Assembly asm = Assembly.LoadFrom("commands/" + cmd);
            foreach(Type T in asm.GetTypes())
            {
                if(T.BaseType == typeof(CommandBase))
                {
                    commands.Add((CommandBase)Activator.CreateInstance(T));
                }
            }
        }
    }
}
