using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Bot
{
    class Logger
    {
        static StreamWriter swlog;
        static StreamWriter swmsg;

        static Logger()
        {
            swlog = new StreamWriter("log.log");
            swmsg = new StreamWriter("msg.log", true);
        }

        internal static async Task Flush()
        {
            await swlog.FlushAsync();
            await swmsg.FlushAsync();
        }

        public static async Task Log(LogMessage message)
        {
            switch(message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogSeverity.Debug:
                    return;
            }
            var msg = $"{DateTime.UtcNow.ToString("dd/MM/yyyy H:mm:ss")} {message}";
            Console.WriteLine(msg);
            Console.ResetColor();
            await swlog.WriteLineAsync(msg);
            await swlog.FlushAsync();
        }

        public static async Task Message(SocketMessage message)
        {
            var channel = message.Channel as SocketGuildChannel;
            string msg;
            if (channel == null)
                msg = $"{message.Timestamp.ToString("dd/MM/yyyy H:mm:ss")} DM @ {message.Author.Username}#{message.Author.Discriminator}: {message}";
            else
                msg = $"{message.Timestamp.ToString("dd/MM/yyyy H:mm:ss")} {(message.Channel as SocketGuildChannel).Guild.Name} #{message.Channel.Name} @ {((message.Author as SocketGuildUser).Nickname == null ? message.Author.Username : (message.Author as SocketGuildUser).Nickname + " " + message.Author.Username)}: {message}";
            Console.WriteLine(msg);
            await swmsg.WriteLineAsync(msg);
            await swmsg.FlushAsync();
        }
    }
}