using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Commands.Permissions;
using Discord.Modules;

namespace discordbot
{
    public abstract class CommandBase
    {
        public string name;
        public string[] aliases;
        public KeyValuePair<string, ParameterType>[] parameters;
        public string description = null;
        public IPermissionChecker[] permissionCheckers;
        public Func<Command, User, Channel, bool>[] permissionFuncs;
        public abstract Task action(CommandEventArgs e);

        void make()
        {
            if (name == null) throw new NotImplementedException();
            CommandBuilder command = Commands.service.CreateCommand(name);
            if (permissionCheckers != null)
            {
                foreach (var check in permissionCheckers)
                {
                    command = command.AddCheck(check);
                }
            }
            if (permissionFuncs != null)
            {
                foreach (var check in permissionFuncs)
                {
                    command = command.AddCheck(check, null);
                }
            }
            if (parameters != null)
            {
                foreach (var kv in parameters)
                {
                    command = command.Parameter(kv.Key, kv.Value);
                }
            }
            command = command.Alias(aliases != null ? aliases : new string[] { });
            command = command.Description(description != null ? description : "");
            command.Do(action);
        }

        public CommandBase(string name,
            string description = null, 
            string[] aliases = null,
            KeyValuePair<string,ParameterType>[] parameters = null,
            IPermissionChecker[] permissionCheckers = null,
            Func<Command, User, Channel, bool>[] permissionFuncs = null)
        {
            this.name = name;
            this.description = description;
            this.aliases = aliases;
            this.parameters = parameters;
            this.permissionCheckers = permissionCheckers;
            this.permissionFuncs = permissionFuncs;
            make();
        }

        public CommandBase()
        {
            make();
        }
    }
}
