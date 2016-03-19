using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using CefSharp.OffScreen;

namespace discordbot.commands
{
    class Web : CommandBase
    {
        public override async Task action(CommandEventArgs e)
        {
            await Task.Yield();
            ChromiumWebBrowser br = new ChromiumWebBrowser(e.GetArg("site"));
            br.Size = new Size(1024, 768);
            EventHandler<CefSharp.LoadingStateChangedEventArgs> handler = null;
            handler = new EventHandler<CefSharp.LoadingStateChangedEventArgs>(async (s, ev) =>
            {
                if (!ev.IsLoading)
                {
                    await Task.Delay(1000);
                    br.LoadingStateChanged -= handler;
                    Bitmap b = await  br.ScreenshotAsync();
                    if (b == null) return;
                    MemoryStream stream = new MemoryStream();
                    b.Save(stream, ImageFormat.Png);
                    stream.Position = 0;
                    await e.Channel.SendFile("web.png", stream);
                    b.Dispose();
                    br.Dispose();
                }
            });
            br.LoadingStateChanged += handler;
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

        public override bool permission(Command command, User user, Channel channel)
        {
            return user.Id == 121183247022555137;
        }

        public Web() : base("web", parameters: new KeyValuePair<string, ParameterType>[]
            {
                new KeyValuePair<string, ParameterType>("site", ParameterType.Required)
            })
        { }
    }
}
