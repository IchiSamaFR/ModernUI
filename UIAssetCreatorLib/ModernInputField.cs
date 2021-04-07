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
    public class ModernInputField : ModernDesigner
    {
        private TextBox textBox = new TextBox();

        private bool multiline = false;
        private string titleText;
        private Color titleColor;
        private Color focusColorBorder;
        private int titlePadding;
        private Font titleFont;
        private string titleAlign;

        public string TitleText
        {
            get { return titleText; }
            set
            {
                titleText = value;
                Refresh();
            }
        }
        public Color TitleColor
        {
            get { return titleColor; }
            set
            {
                titleColor = value;
                Refresh();
            }
        }
        public Color FocusColorBorder
        {
            get { return focusColorBorder; }
            set
            {
                focusColorBorder = value;
                Refresh();
            }
        }
        public int TitlePadding
        {
            get { return titlePadding; }
            set
            {
                titlePadding = value;
                Refresh();
            }
        }
        public Font TitleFont
        {
            get { return titleFont; }
            set
            {
                titleFont = value;
                Refresh();
            }
        }
        public string TitleAlign
        {
            get { return titleAlign; }
            set
            {
                titleAlign = value;
                Refresh();
            }
        }
        public bool Multiline
        {
            get { return multiline; }
            set
            {
                multiline = value;
                Refresh();
            }
        }



        public ModernInputField() : base()
        {
            textBox.GotFocus += FocusChanged;
            textBox.LostFocus += FocusChanged;
            GotFocus += FocusChanged;
            LostFocus += FocusChanged;

            Text = "ModernInputField";
            titleText = "ModernInputField";
            titleColor = ModernConfiguration.main_color;
            titlePadding = 5;
            titleAlign = "left";
            TextPadding = 5;


            TitleFont = new Font(
                           TextFont.FontFamily,
                           TextFont.Size + 2,
                           FontStyle.Bold,
                           GraphicsUnit.Pixel);
            BorderColor = ModernConfiguration.main_color;
            FillColor = Color.FromArgb(250, 250, 250);
            BackColor = Color.Transparent;
            FontColor = ModernConfiguration.titlefont_color;
            FontColor = Color.Black;
            FocusColorBorder = ModernConfiguration.secondary_color;

            this.Controls.Add(textBox);
            Size = new Size(200, 40);
        }
        private void FocusChanged(object sender, EventArgs e)
        {
            Refresh();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Color foc = BorderColor;
            if ((Focused || textBox.Focused) && ParentEnabled)
            {
                foc = FocusColorBorder;
            }
            else if (!ParentEnabled)
            {
                foc = Color.FromArgb(ModernConfiguration.AlphaValue, BorderColor.R, BorderColor.G, BorderColor.B);
            }

            var path = new GraphicsPath();

            SizeF stringSizeF = e.Graphics.MeasureString(TitleText, TextFont);
            Size stringSize = new Size((int)Math.Ceiling(stringSizeF.Width), (int)Math.Ceiling(stringSizeF.Height));
            int halfStringSize = stringSize.Height / 2;

            if (ParentEnabled)
            {
                CreateRecObject(e, path, 0, halfStringSize, Size.Width, Size.Height - halfStringSize, FillColor);

                if (BorderWidth > 0)
                {
                    CreateRecObject(e, path, BorderWidth, BorderWidth + halfStringSize, Size.Width - BorderWidth * 2, Size.Height - BorderWidth * 2 - halfStringSize, foc);
                }
            }
            else
            {
                CreateRecObject(e, path, 0, halfStringSize, Size.Width, Size.Height - halfStringSize, Color.FromArgb(ModernConfiguration.AlphaValue, FillColor.R, FillColor.G, FillColor.B));

                if (BorderWidth > 0)
                {
                    CreateRecObject(e, path, BorderWidth, BorderWidth + halfStringSize, Size.Width - BorderWidth * 2, Size.Height - BorderWidth * 2 - halfStringSize, foc);
                }
            }
            using (Brush aBrush = new SolidBrush(FontColor))
            {
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;

                if (TitleAlign.ToLower() == "left")
                {
                    format.Alignment = StringAlignment.Center;
                    Rectangle rec = new Rectangle(BorderRadius / 2 + TitlePadding,
                                                  ClientRectangle.Y,
                                                  stringSize.Width,
                                                  halfStringSize * 2);
                    GraphicsPath newPath = new GraphicsPath();
                    newPath.AddRectangle(rec);
                    e.Graphics.FillPath(new SolidBrush(FillColor), newPath);
                    e.Graphics.DrawString(TitleText, TitleFont, new SolidBrush(foc), rec, format);
                }
                else if (TitleAlign.ToLower() == "right")
                {
                    format.Alignment = StringAlignment.Center;
                    Rectangle rec = new Rectangle(ClientRectangle.Width - (BorderRadius / 2 + TitlePadding + stringSize.Width),
                                                  ClientRectangle.Y,
                                                  stringSize.Width,
                                                  halfStringSize * 2);
                    GraphicsPath newPath = new GraphicsPath();
                    newPath.AddRectangle(rec);
                    e.Graphics.FillPath(new SolidBrush(FillColor), newPath);
                    e.Graphics.DrawString(TitleText, TitleFont, new SolidBrush(foc), rec, format);
                }
                else if (TitleAlign.ToLower() == "center")
                {
                    format.Alignment = StringAlignment.Center;
                    Rectangle rec = new Rectangle((ClientRectangle.Width - stringSize.Width) / 2,
                                                  ClientRectangle.Y,
                                                  ClientRectangle.Width - (ClientRectangle.Width - stringSize.Width),
                                                  halfStringSize * 2);
                    GraphicsPath newPath = new GraphicsPath();
                    newPath.AddRectangle(rec);
                    e.Graphics.FillPath(new SolidBrush(FillColor), newPath);
                    e.Graphics.DrawString(TitleText, TitleFont, new SolidBrush(foc), rec, format);
                }
                else
                {
                    TitleAlign = "left";
                    format.Alignment = StringAlignment.Center;
                    Rectangle rec = new Rectangle(BorderRadius / 2 + TitlePadding,
                                                  ClientRectangle.Y,
                                                  stringSize.Width,
                                                  halfStringSize * 2);
                    GraphicsPath newPath = new GraphicsPath();
                    newPath.AddRectangle(rec);
                    e.Graphics.FillPath(new SolidBrush(FillColor), newPath);
                    e.Graphics.DrawString(TitleText, TitleFont, new SolidBrush(foc), rec, format);
                }
            }
            path.Dispose();


            textBox.Multiline = multiline;
            textBox.BackColor = FillColor;
            //textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BorderStyle = BorderStyle.None;
            if (multiline)
            {
                textBox.Size = new Size(ClientRectangle.Width - (BorderRadius + TextPadding * 2),
                                        ClientRectangle.Height - (BorderRadius + TextPadding * 2));
                textBox.Location = new Point(BorderRadius / 2 + TextPadding,
                                             BorderRadius / 2 + TextPadding + stringSize.Height / 2);
            }
            else
            {
                textBox.Size = new Size(ClientRectangle.Width - (BorderRadius + TextPadding * 2),
                                        ClientRectangle.Height - (BorderRadius + TextPadding * 2 + stringSize.Height / 2));
                textBox.Location = new Point(BorderRadius / 2 + TextPadding,
                                             ClientRectangle.Height / 2 - textBox.Size.Height / 2 + stringSize.Height / 2);
            }
            if (ParentEnabled)
                textBox.Enabled = true;
            else
                textBox.Enabled = false;
        }


    }
}
