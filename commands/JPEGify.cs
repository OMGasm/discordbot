using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
namespace discordbot.commands
{
    class JPEGify : CommandBase
    {
        private async Task jpegify(CommandEventArgs e, long compression, Stream image)
        {
            Bitmap b = new Bitmap(image);
            image.Close();
            MemoryStream stream = new MemoryStream();
            EncoderParameters p = new EncoderParameters(1);
            p.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compression);
            b.Save(stream, GetEncoder(ImageFormat.Jpeg), p);
            stream.Position = 0;
            await e.Channel.SendFile("jpegged.jpg", stream);
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

        public override async Task action(CommandEventArgs e)
        {
            double compression;
            string url = null;
            if (e.GetArg("level").ToLower().Equals("me"))
            {
                url = e.User.AvatarUrl;
                compression = 2 * Math.Log(Sexymeter.calculate(e.User.Name), 1.2);
            }
            else if (e.GetArg("level").ToLower().Equals("yourself"))
            {
                url = Program.client.CurrentUser.AvatarUrl;
                compression = 2 * Math.Log(Sexymeter.calculate(e.User.Name), 1.2);
            }
            else if (!double.TryParse(e.GetArg("level"), out compression))
            {
                return;
            }
            compression = Math.Min(Math.Max(compression, 0), 100);

            if (url == null)
            {
                if (e.GetArg("image") == "")
                {
                    if (e.Message.Attachments.Length > 0)
                    {
                        url = e.Message.Attachments[0].Url;
                    }
                    else if (e.Message.Embeds.Length > 0)
                    {
                        if (e.Message.Embeds[0].Type.ToLower().Equals("image"))
                            url = e.Message.Embeds[0].Url;
                        else return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    url = e.GetArg("image");
                }
            }

            WebRequest wr = WebRequest.CreateHttp(url);
            url = null;
            WebResponse re = await wr.GetResponseAsync();
            if(re.ContentLength>0)
            {
                switch(re.ContentType)
                {
                    case "image/bmp":
                    case "image/x-windows-bmp":
                    case "image/gif":
                    case "image/jpeg":
                    case "image/png":
                        {
                            await jpegify(e, (long)Math.Round(compression), re.GetResponseStream());
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

        public JPEGify() : base("jpegify",
            "Make pro 1965 JPEG images",
            parameters: new KeyValuePair<string, ParameterType>[] {
                new KeyValuePair<string, ParameterType>("level", ParameterType.Required),
                new KeyValuePair<string, ParameterType>("image", ParameterType.Optional) })
        { }
    }
}
