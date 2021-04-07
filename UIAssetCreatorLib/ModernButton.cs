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

using UIAssetsCreator;

namespace UIAssetsCreator.Assets
{
    public class ModernButton : ModernDesigner
    {
        public ModernButton() : base()
        {
            BorderRadius = 40;
            Text = "ModernButton";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var path = new GraphicsPath();

            if (Enabled && ParentEnabled)
            {
                if (isDown)
                {
                    CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, Color.FromArgb(Tool.Clamp((int)(FillColor.R * 1.05f), 0, 255),
                                                                                        Tool.Clamp((int)(FillColor.G * 1.05f), 0, 255),
                                                                                        Tool.Clamp((int)(FillColor.B * 1.05f), 0, 255)));
                }
                else if (isHover)
                {
                    CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, Color.FromArgb(Tool.Clamp((int)(FillColor.R * 1.1f), 0, 255),
                                                                                        Tool.Clamp((int)(FillColor.G * 1.1f), 0, 255),
                                                                                        Tool.Clamp((int)(FillColor.B * 1.1f), 0, 255)));
                }
                else
                {
                    CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, FillColor);
                }
                if (BorderWidth > 0)
                {
                    CreateRecObject(e, path, BorderWidth, BorderWidth, Size.Width - BorderWidth * 2, Size.Height - BorderWidth * 2, BorderColor);
                }
            }
            else
            {
                CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, Color.FromArgb(ModernConfiguration.AlphaValue, FillColor.R, FillColor.G, FillColor.B));

                if (BorderWidth > 0)
                {
                    CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, Color.FromArgb(ModernConfiguration.AlphaValue, BorderColor.R, BorderColor.G, BorderColor.B));
                }
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
                                                  ClientRectangle.Height);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                }
                else if (TextAlign.ToLower() == "right")
                {
                    format.Alignment = StringAlignment.Far;
                    Rectangle rec = new Rectangle(ClientRectangle.X + TextPadding + BorderWidth,
                                                  ClientRectangle.Y,
                                                  ClientRectangle.Width - (TextPadding + BorderWidth) * 2,
                                                  ClientRectangle.Height);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                }
                else if (TextAlign.ToLower() == "center")
                {
                    format.Alignment = StringAlignment.Center;
                    Rectangle rec = new Rectangle(ClientRectangle.X + TextPadding + BorderWidth,
                                                  ClientRectangle.Y,
                                                  ClientRectangle.Width - (TextPadding + BorderWidth) * 2,
                                                  ClientRectangle.Height);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                }
                else
                {
                    TextAlign = "center";
                    format.Alignment = StringAlignment.Center;
                    Rectangle rec = new Rectangle(ClientRectangle.X + TextPadding + BorderWidth,
                                                  ClientRectangle.Y,
                                                  ClientRectangle.Width - (TextPadding + BorderWidth) * 2,
                                                  ClientRectangle.Height);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                }
            }

            path.Dispose();
        }
    }
}
