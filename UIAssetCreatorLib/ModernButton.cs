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
        bool readOnly = false;
        Color hoverColor = new Color();
        Color downColor = new Color();
        Color hoverFont = new Color();
        Color downFont = new Color();
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
            }
        }
        public Color HoverColor
        {
            get { return hoverColor; }
            set
            {
                hoverColor = value;
            }
        }
        public Color DownColor
        {
            get { return downColor; }
            set
            {
                downColor = value;
            }
        }
        public Color HoverFont
        {
            get { return hoverFont; }
            set
            {
                hoverFont = value;
            }
        }
        public Color DownFont
        {
            get { return downFont; }
            set
            {
                downFont = value;
            }
        }

        public ModernButton() : base()
        {
            BorderWidth = 0;
            BorderRadius = 40;
            HoverColor = Color.Transparent;
            DownColor = Color.Transparent;
            HoverFont = Color.Transparent;
            DownFont = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var path = new GraphicsPath();

            if (Enabled && ParentEnabled && !ReadOnly)
            {
                if (isDown)
                {
                    Color toSet = DownColor == Color.Transparent ? Color.FromArgb(Tool.Clamp((int)(FillColor.R * 1.05f), 0, 255),
                                                                                        Tool.Clamp((int)(FillColor.G * 1.05f), 0, 255),
                                                                                        Tool.Clamp((int)(FillColor.B * 1.05f), 0, 255)) : DownColor;
                    CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, toSet);
                }
                else if (isHover)
                {
                    Color toSet = hoverColor == Color.Transparent ? Color.FromArgb(Tool.Clamp((int)(FillColor.R * 1.1f), 0, 255),
                                                                                        Tool.Clamp((int)(FillColor.G * 1.1f), 0, 255),
                                                                                        Tool.Clamp((int)(FillColor.B * 1.1f), 0, 255)) : hoverColor;
                    
                    CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, toSet);
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
            else if(ReadOnly)
            {
                CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, FillColor);

                if (BorderWidth > 0)
                {
                    CreateRecObject(e, path, 0, 0, Size.Width, Size.Height, BorderColor);
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



            Color color = new Color();

            if (isDown && downFont != null)
                color = downFont == Color.Transparent ? FontColor : downFont;
            else if (isHover && hoverFont != null)
                color = hoverFont == Color.Transparent ? FontColor : hoverFont;
            else
                color = FontColor;

            using (Brush aBrush = new SolidBrush(color))
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

            if(ImageBack != null)
            {
                if (ImageSize.Width > Width) ImageSize = new Size(Width, ImageSize.Height);
                if (ImageSize.Height > Height) ImageSize = new Size(ImageSize.Height, Height);

                e.Graphics.DrawImage(ImageBack, (Width - ImageSize.Width) / 2, (Height - ImageSize.Height) / 2, ImageSize.Width, ImageSize.Height);
            }

            path.Dispose();
        }

        public override void OnEnter(object sender, EventArgs e)
        {
            base.OnEnter(sender, e);
            Refresh();
        }
        public override void OnLeave(object sender, EventArgs e)
        {
            base.OnLeave(sender, e);
            Refresh();
        }
        public override void OnDown(object sender, EventArgs e)
        {
            base.OnDown(sender, e);
            Refresh();
        }
        public override void OnUp(object sender, EventArgs e)
        {
            base.OnUp(sender, e);
            Refresh();
        }

        public override void OnClick(object sender, EventArgs e)
        {
            if (ReadOnly || !Enabled || !ParentEnabled) return;
            base.OnClick(sender, e);
        }
    }
}
