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
            string time = message.Timestamp.ToString("dd/MM/yyyy H:mm:ss");
            string msg = message.ToString().Replace("\u0007", "").Replace("\u2022", "");
            string user;
            string formattedMsg;
            string discrim = message.Author.Discriminator;
            if (channel == null)
            {
                user = message.Author.Username;
                formattedMsg = $"{time} DM @ {user}#{discrim}: {msg}";
            }
            else
            {
                string guild = channel.Guild.Name;
                user = (message.Author as IGuildUser)?.Nickname ?? message.Author.Username;
                formattedMsg = $"{time} {guild} #{channel.Name} @ {user}: {msg}";
            }
            Console.WriteLine(formattedMsg);
            await swmsg.WriteLineAsync(formattedMsg);
            await swmsg.FlushAsync();
        }
    }
}