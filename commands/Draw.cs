using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;
namespace discordbot.commands
{
    class Draw : CommandBase
    {
        static Font f;

        internal static void setFont(string s)
        {
            Font old = f;
            FontFamily nff;
            nff = new FontFamily(s);
            if (nff.IsStyleAvailable(FontStyle.Regular))
                f = new Font(nff, 11f, FontStyle.Regular);
            else if (nff.IsStyleAvailable(FontStyle.Bold))
                f = new Font(nff, 11f, FontStyle.Bold);
            else
                f = old;
        }

        public override async Task action(CommandEventArgs e)
        {
            if (e.Args.Length == 0) return;
            Size s = TextRenderer.MeasureText(e.GetArg("text"), f, new Size(500, 1000), TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak);
            Bitmap b = new Bitmap(s.Width, s.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.DrawString(e.GetArg("text")+'\n', f, new SolidBrush(Color.FromArgb(0, 137, 255)), new Rectangle(0,0, s.Width,s.Height), new StringFormat(StringFormatFlags.FitBlackBox));
            g.Flush();
            var stream = new MemoryStream();
            b.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            await e.Channel.SendFile("text.png", stream);
        }

        public Draw() : base("draw",
            parameters: new KeyValuePair<string, ParameterType>[]
            {
                new KeyValuePair<string, ParameterType>("text", ParameterType.Unparsed)
            },
            permissionFuncs: new Func<Command, Discord.User, Discord.Channel, bool>[]
            {
                (user, chan, pass) => true
            })
        {
            FontFamily[] fonts = new FontFamily[]
            {
                new FontFamily("Whitney Medium"),
                //new FontFamily("Arial")
            };
            f = new Font(fonts.First(font => font.IsStyleAvailable(FontStyle.Regular)), 11f, FontStyle.Regular);
        }
    }
}
