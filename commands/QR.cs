using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.IO;
using ZXing;

namespace discordbot.commands
{
    class QR : CommandBase
    {
        public override async Task action(CommandEventArgs e)
        {
            string s = e.GetArg("target");
            if (s.Equals("")) return;
            else await Task.Yield();

            BarcodeWriter bw = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE
            };
            using (var stream = new MemoryStream())
            using (var bitmap = bw.Write(s))
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                await e.Channel.SendFile("qr.png", stream);
            }
        }

        public QR() : base("qr", 
            parameters: new KeyValuePair<string, ParameterType>[] 
            { new KeyValuePair<string, ParameterType>("target", ParameterType.Unparsed) })
        { }
    }
}
