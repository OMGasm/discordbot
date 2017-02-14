using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.IO;
using ZXing;

namespace Bot.commands
{
    public class QR : ModuleBase
    {
        public async Task action(string target)
        {
            BarcodeWriter bw = new BarcodeWriter()
            {
                 Format = BarcodeFormat.QR_CODE
            };
            using (var stream = new MemoryStream())
            using (var bitmap = bw.Write(target))
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                await Context.Channel.SendFileAsync(stream, "qr.png");
            }
        }
    }
}
