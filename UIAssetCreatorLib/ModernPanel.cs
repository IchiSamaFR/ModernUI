using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ComponentModel.Design;

using UIAssetsCreator;

namespace UIAssetsCreator.Assets
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class ModernPanel : ModernDesigner
    {
        private int titleHeight = 30;

        public int TitleHeight
        {
            get { return titleHeight; }
            set
            {
                titleHeight = value;
                Refresh();
            }
        }

        public ModernPanel() : base()
        {
            BorderWidth = 1;
            Size = new Size(150, 200);
            BorderColor = FillColor;
            FillColor = Color.White;
            Text = "ModernPanel";
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var path = new GraphicsPath();

            if (Enabled && ParentEnabled)
            {
                CreateRecObject_DownBorder(e, path, BorderWidth, titleHeight, Size.Width - BorderWidth, Size.Height - BorderWidth - titleHeight, FillColor);
                CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, BorderColor);
            }
            else
            {
                CreateRecObject_DownBorder(e, path, BorderWidth, titleHeight, Size.Width - BorderWidth, Size.Height - BorderWidth - titleHeight, Color.FromArgb(ModernConfiguration.AlphaValue, FillColor.R, FillColor.G, FillColor.B));
                CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, Color.FromArgb(ModernConfiguration.AlphaValue, BorderColor.R, BorderColor.G, BorderColor.B));
            }

            using (Brush aBrush = new SolidBrush(FontColor))
            {
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;

                if (TextAlign.ToLower() == "left")
                {
                    format.Alignment = StringAlignment.Near;
                    Rectangle rec = new Rectangle(ClientRectangle.X + TextPadding + BorderWidth,
                                                  ClientRectangle.Y,
                                                  ClientRectangle.Width - (TextPadding + BorderWidth) * 2,
                                                  titleHeight);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                }
                else if (TextAlign.ToLower() == "right")
                {
                    format.Alignment = StringAlignment.Far;
                    Rectangle rec = new Rectangle(ClientRectangle.X + TextPadding + BorderWidth,
                                                  ClientRectangle.Y,
                                                  ClientRectangle.Width - (TextPadding + BorderWidth) * 2,
                                                  titleHeight);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                }
                else if (TextAlign.ToLower() == "center")
                {
                    format.Alignment = StringAlignment.Center;
                    Rectangle rec = new Rectangle(ClientRectangle.X + TextPadding + BorderWidth,
                                                  ClientRectangle.Y,
                                                  ClientRectangle.Width - (TextPadding + BorderWidth) * 2,
                                                  titleHeight);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                }
                else
                {
                    TextAlign = "center";
                    format.Alignment = StringAlignment.Center;
                    Rectangle rec = new Rectangle(ClientRectangle.X + TextPadding + BorderWidth,
                                                  ClientRectangle.Y,
                                                  ClientRectangle.Width - (TextPadding + BorderWidth) * 2,
                                                  titleHeight);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                }
            }

            path.Dispose();
        }
    }
}
