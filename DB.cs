using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Linq;
using Discord;

namespace discordbot
{
    class DB
    {
        static SQLiteConnection connection;

        static void makeTables()
        {
            StreamReader s = new StreamReader("sql/tables.sql");
            var cmd = new SQLiteCommand(s.ReadToEnd(), connection);
            cmd.ExecuteNonQuery();
        }

        static DB()
        {
            connection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
            connection.Open();
            makeTables();
        }

        internal static async Task writeLog(LogMessageEventArgs e)
        {
            var context = new DataContext(connection);
            var logs = context.GetTable<db.Log>();
            var log = new db.Log();
            log.time = (long)TimeSpan.FromTicks(DateTime.UtcNow.Ticks).TotalSeconds;
            log.severity = (int)e.Severity;
            log.source = e.Source;
            log.message = e.Message;
            logs.InsertOnSubmit(log);
            context.SubmitChanges();
            await Task.Yield();
        }

        internal static async Task addVideo(ulong messageID, Message.Embed e)
        {
            using (var context = new DataContext(connection))
            {
                var videos = context.GetTable<db.EmbedVideo>();
                var video = new db.EmbedVideo();
                video.embedID = messageID;
                video.url = e.Video.Url;
                video.proxyURL = e.Video.ProxyUrl;
                video.width = e.Video.Width;
                video.height = e.Video.Height;
                videos.InsertOnSubmit(video);
                context.SubmitChanges();
            }
            await Task.Yield();
        }

        internal static async Task addEmbed(ulong messageID, Message.Embed e)
        {
            using (var context = new DataContext(connection))
            {
                var embeds = context.GetTable<db.Embed>();
                var embed = new db.Embed();
                embed.messageID = messageID;
                embed.title = e.Title;
                embed.description = e.Description;
                embed.url = e.Url;
                if (e.Provider != null)
                {
                    embed.providerURL = e.Provider.Url;
                    embed.providerName = e.Provider.Name;
                }
                if (e.Thumbnail != null)
                {
                    embed.thumbnailURL = e.Thumbnail.Url;
                    embed.thumbnailProxyURL = e.Thumbnail.ProxyUrl;
                    embed.thumbnailWidth = e.Thumbnail.Width;
                    embed.thumbnailHeight = e.Thumbnail.Height;
                }
                embeds.InsertOnSubmit(embed);
                context.SubmitChanges();
            }
            if (e.Video != null && e.Video.Height.HasValue)
            {
                await addVideo(messageID, e);
            }
            await Task.Yield();
        }

        internal static async Task addAttachment(ulong messageID, Message.Attachment e)
        {
            using (var context = new DataContext(connection))
            {
                var attachments = context.GetTable<db.Attachment>();
                var attach = new db.Attachment();
                attach.attachmentID = e.Id;
                attach.messageID = messageID;
                attach.filename = e.Filename;
                attach.proxyURL = e.ProxyUrl;
                attach.url = e.Url;
                attach.size = e.Size;
                attachments.InsertOnSubmit(attach);
            }
            await Task.Yield();
        }

        internal static async Task writeMessage(MessageEventArgs e)
        {
            using (var context = new DataContext(connection))
            {
                var messages = context.GetTable<db.Message>();
                var message = new db.Message();
                message.id = e.Message.Id;
                message.time = (long)TimeSpan.FromTicks(e.Message.Timestamp.Ticks).TotalSeconds;
                message.channelID = e.Channel.Id;
                message.userID = e.User.Id;
                message.message = e.Message.Text;
                messages.InsertOnSubmit(message);
                context.SubmitChanges();
            }

            foreach (var attachment in e.Message.Attachments)
            {
                await addAttachment(e.Message.Id, attachment);
            }

            foreach (var embed in e.Message.Embeds)
            {
                await addEmbed(e.Message.Id, embed);
            }

            await Task.Yield();
        }
    }
}
