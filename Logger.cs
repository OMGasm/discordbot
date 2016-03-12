using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace discordbot
{
    class Logger
    {
        internal static async Task log(LogMessageEventArgs e)
        {
            await (DB.writeLog(e));
            if(e.Severity <= LogSeverity.Warning)
            {
                Console.ForegroundColor = e.Severity == LogSeverity.Error ? ConsoleColor.Red : ConsoleColor.Yellow;
                Console.WriteLine("{0}: {1}: {2}", e.Severity, e.Source, e.Message);
                Console.ResetColor();
            }
        }
        
        internal static async Task log(MessageEventArgs e)
        {
            await DB.writeMessage(e);
            Console.WriteLine("{0}: {1}: {2}", e.Server, e.Channel, e.Message);
        }
    }
}
