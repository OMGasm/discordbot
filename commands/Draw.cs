using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;
using Discord.Commands;

namespace Bot.commands
{
    public class Draw : ModuleBase
    {
        static Font f;
        static Color c;

        [Command("draw")]
        public async Task action([Remainder] string text)
        {
            const int len = 79;
            for (int i = 0, c = 0; i < text.Length; i++, c++)
            {
                if (text[i] == '\n') c = 0;
                if (c == len)
                {
                    text = text.Substring(0, i) + '\n' + text.Substring(i, text.Length - i);
                    c = 0;
                }
            }

            Size s = TextRenderer.MeasureText(text, f);
            Bitmap b = new Bitmap(s.Width, s.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.DrawString(text, f, new SolidBrush(c), 0, 0);
            g.Flush();
            var stream = new MemoryStream();
            b.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            await Context.Channel.SendFileAsync(stream, "text.png");
        }

        public Draw()
        {
            FontFamily[] fonts = new FontFamily[]
            {
                new FontFamily("Whitney Medium"),
                //new FontFamily("Arial")
            };
            f =  f ?? new Font(fonts.First(font => font.IsStyleAvailable(FontStyle.Regular)), 11f, FontStyle.Regular);
            c = Color.FromArgb(0, 137, 255);
        }

        internal void setFont(string s, float size)
        {
            Font old = f;
            FontFamily nff;
            try
            {
                nff = new FontFamily(s);
                if (nff.IsStyleAvailable(FontStyle.Regular))
                    f = new Font(nff, size, FontStyle.Regular);
                else if (nff.IsStyleAvailable(FontStyle.Bold))
                    f = new Font(nff, size, FontStyle.Bold);
                else
                    f = old;
            }
            catch (Exception e)
            {
                Equals(e, e);
                f = old;
            }
        }

        [RequireOwner]
        [Command("fntc"), Summary("Change font colour")]
        public async Task ChangeFontColour(string colour)
        {
            string[] s = colour.Split(',');
            if(s.Length==1 && s[0].Length == 6)
            {
                int r, g, b;
                var X = System.Globalization.NumberStyles.AllowHexSpecifier;
                var F = new System.Globalization.NumberFormatInfo();
                if (!int.TryParse(s[0].Take(2).ToString(), X, F, out r)) return;
                if (!int.TryParse(s[0].Skip(2).Take(2).ToString(), X, F, out g)) return;
                if (!int.TryParse(s[0].Skip(4).Take(2).ToString(), X, F, out b)) return;
                c = Color.FromArgb(r, g, b);
            }
            await Task.Delay(0);
        }

        [RequireOwner]
        [Command("fnt"), Summary("Change font")]
        public async Task ChangeFont([Remainder] string font)
        {
            await Task.Delay(0);
            string[] fn = font.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            float fsize;
            if (fn.Length > 1)
            {
                fsize = float.Parse(fn[1]);
                fsize = fsize > 0 ? fsize : 11f;
                if (fn[0].Length > 1)
                {
                    setFont(fn[0], fsize);
                }
            }
            else
            {
                if (fn[0].Length > 1)
                {
                    setFont(fn[0], 11f);
                }
            }
        }
    }
}
