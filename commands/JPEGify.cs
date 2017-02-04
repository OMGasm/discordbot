using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Text.RegularExpressions;
using Discord.Commands;

namespace Bot.commands
{
    public class JPEGify : ModuleBase
    {
        private async Task jpegify(ICommandContext context, long compression, Stream image)
        {
            Bitmap b = new Bitmap(image);
            image.Close();
            MemoryStream stream = new MemoryStream();
            EncoderParameters p = new EncoderParameters(1);
            p.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compression);
            b.Save(stream, GetEncoder(ImageFormat.Jpeg), p);
            stream.Position = 0;
            await context.Channel.SendFileAsync(stream, "jpegged.jpg");
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        [Command("jpegify"), Summary("Do I look like I know what a jay-peg is?")]
        public async Task action(string level = "", string image = "")
        {
            double compression;
            string url = null;
            Regex r = new Regex("<@!?([0-9]+)>");
            ulong userid;
            if (level.Equals("me"))
            {
                url = Context.User.AvatarUrl;
                compression = 2 * Math.Log(Sexymeter.calculate(Context.User.Username), 1.2);
            }
            else if (Discord.MentionUtils.TryParseUser(level, out userid))
            {
                Discord.IGuildUser user = await Context.Guild.GetUserAsync(userid);
                url = user.AvatarUrl;
                compression = 2 * Math.Log(Sexymeter.calculate(user.Username), 1.2);
            }
            else if (level.ToLower().Equals("yourself"))
            {
                url = Bot.Client.CurrentUser.AvatarUrl;
                compression = 2 * Math.Log(Sexymeter.calculate(Context.User.Username), 1.2);
            }
            else if (!double.TryParse(level, out compression))
            {
                return;
            }
            compression = Math.Min(Math.Max(compression, 0), 100);

            if (url == null)
            {
                if (image == "")
                {
                    if (Context.Message.Attachments.Count > 0)
                    {
                        url = Context.Message.Attachments.First().Url;
                    }
                    else if (Context.Message.Embeds.Count > 0)
                    {
                        if (Context.Message.Embeds.First().Type.ToLower().Equals("image"))
                            url = Context.Message.Embeds.First().Url;
                        else return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    url = image;
                }
            }
            WebRequest wr = WebRequest.CreateHttp(url);
            url = null;
            WebResponse re = await wr.GetResponseAsync();
            if (re.ContentLength > 0)
            {
                switch (re.ContentType)
                {
                    case "image/bmp":
                    case "image/x-windows-bmp":
                    case "image/gif":
                    case "image/jpeg":
                    case "image/png":
                        {
                            await jpegify(Context, (long)Math.Round(compression), re.GetResponseStream());
                            re.Dispose();
                            break;
                        }
                    default:
                        {
                            re.Dispose();
                            return;
                        }
                }
            }
        }
    }
}
