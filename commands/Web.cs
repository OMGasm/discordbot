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

namespace Bot.commands
{
    public class Web : ModuleBase
    {
        public async Task action([Remainder]string site)
        {
            if (site == "") return;
            await Task.Delay(0);
            ChromiumWebBrowser br = new ChromiumWebBrowser(site);
            //br.Size = new Size(1024, 768);
            EventHandler<CefSharp.LoadingStateChangedEventArgs> handler = null;
            handler = new EventHandler<CefSharp.LoadingStateChangedEventArgs>(async (s, ev) =>
            {
                while (ev.IsLoading) await Task.Delay(100);
                {
                    br.LoadingStateChanged -= handler;
                    Bitmap b = await br.ScreenshotAsync();
                    if (b == null)
                    {
                        br.Dispose();
                        return;
                    }
                    MemoryStream stream = new MemoryStream();
                    b.Save(stream, ImageFormat.Png);
                    stream.Position = 0;
                    await Context.Channel.SendFileAsync(stream, "web.png");
                    b.Dispose();
                    br.Dispose();
                }
            });
            br.LoadingStateChanged += handler;
        }
    }
}
