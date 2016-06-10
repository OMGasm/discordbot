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
        static Color c;

        internal static void setFont(string s, float size)
        {
            Font old = f;
            FontFamily nff;
            nff = new FontFamily(s);
            if (nff.IsStyleAvailable(FontStyle.Regular))
                f = new Font(nff, size, FontStyle.Regular);
            else if (nff.IsStyleAvailable(FontStyle.Bold))
                f = new Font(nff, size, FontStyle.Bold);
            else
                f = old;
        }

        public override async Task action(CommandEventArgs e)
        {
            if (e.Args.Length == 0) return;
            string str = e.GetArg("text");

            const int len = 79;
            for (int i = 0, c = 0; i < str.Length; i++, c++)
            {
                if (str[i] == '\n') c = 0;
                if (c == len)
                {
                    str = str.Substring(0, i) + '\n' + str.Substring(i, str.Length - i);
                    c = 0;
                }
            }

            Size s = TextRenderer.MeasureText(str, f);
            Bitmap b = new Bitmap(s.Width, s.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.DrawString(str, f, new SolidBrush(c), 0, 0);
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
            c = Color.FromArgb(0, 137, 255);
        }
    }
}
