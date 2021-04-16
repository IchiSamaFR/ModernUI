﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private bool alphaAccepted = true;
        private bool specialAccepted = true;
        private bool numAccepted = true;
        private bool mod = false;

        public string TitleText
        {
            get { return titleText; }
            set
            {
                if (titleText == value)
                    return;
                titleText = value;
                Refresh();
            }
        }
        public Color TitleColor
        {
            get { return titleColor; }
            set
            {
                if (titleColor == value)
                    return;
                titleColor = value;
                Refresh();
            }
        }
        public Color FocusColorBorder
        {
            get { return focusColorBorder; }
            set
            {
                if (focusColorBorder == value)
                    return;
                focusColorBorder = value;
                Refresh();
            }
        }
        public int TitlePadding
        {
            get { return titlePadding; }
            set
            {
                if (titlePadding == value)
                    return;
                titlePadding = value;
                Refresh();
            }
        }
        public Font TitleFont
        {
            get { return titleFont; }
            set
            {
                if (titleFont == value)
                    return;
                titleFont = value;
                Refresh();
            }
        }
        public string TitleAlign
        {
            get { return titleAlign; }
            set
            {
                if (titleAlign == value)
                    return;
                titleAlign = value;
                Refresh();
            }
        }
        public bool Multiline
        {
            get { return multiline; }
            set
            {
                if (multiline == value)
                    return;
                multiline = value;
                Refresh();
            }
        }
        public bool AlphaAccepted
        {
            get { return alphaAccepted; }
            set
            {
                if (alphaAccepted == value)
                    return;
                alphaAccepted = value;
                CheckInputText();
            }
        }
        public bool SpecialAccepted
        {
            get { return specialAccepted; }
            set
            {
                if (specialAccepted == value)
                    return;
                specialAccepted = value;
                CheckInputText();
            }
        }
        public bool NumAccepted
        {
            get { return numAccepted; }
            set
            {
                if (numAccepted == value)
                    return;
                numAccepted = value;
                CheckInputText();
            }
        }
        public override string Text
        {
            get { return textBox.Text; }
            set
            {
                if (textBox.Text == value)
                    return;
                textBox.Text = value;
                if(!mod)
                    CheckInputText();
            }
        }
        public bool ReadOnly
        {
            get { return textBox.ReadOnly; }
            set
            {
                textBox.ReadOnly = value;
                if (textBox.ReadOnly)
                {
                    textBox.Cursor = Cursors.Default;
                }
                else
                {
                    textBox.Cursor = Cursors.IBeam;
                }
            }
        }

        public ModernInputField() : base()
        {
            if (textBox == null)
                textBox = new TextBox();

            textBox.GotFocus += FocusChanged;
            textBox.LostFocus += FocusChanged;
            GotFocus += FocusChanged;
            LostFocus += FocusChanged;

            textBox.KeyPress += CheckInputText;
            
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

            SetEvents();
        }
        private void FocusChanged(object sender, EventArgs e)
        {
            if (ReadOnly)
            {
                Parent.Focus();
                return;
            }

            if(Focused)
                textBox.Focus();
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
                                             BorderRadius / 2 + TextPadding + stringSize.Height / 3);
            }
            else
            {
                textBox.Size = new Size(ClientRectangle.Width - (BorderRadius + TextPadding * 2),
                                        ClientRectangle.Height - (BorderRadius + TextPadding * 2 + stringSize.Height / 3));
                textBox.Location = new Point(BorderRadius / 2 + TextPadding,
                                             ClientRectangle.Height / 2 - textBox.Size.Height / 2 + stringSize.Height / 3);
            }
            if (ParentEnabled)
                textBox.Enabled = true;
            else
                textBox.Enabled = false;
        }

        public void CheckInputText()
        {
            mod = true;
            if (!numAccepted)
            {
                Regex rgx = new Regex("[0-9]");
                Text = rgx.Replace(Text, "");
            }
            if (!alphaAccepted)
            {
                Regex rgx = new Regex("[a-zA-Z]");
                Text = rgx.Replace(Text, "");
            }
            if (!specialAccepted)
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 .]");
                Text = rgx.Replace(Text, "");
            }
            mod = false;
        }
        public void CheckInputText(object sender, KeyPressEventArgs e)
        {
            if (!alphaAccepted && char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!numAccepted && char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if (numAccepted && !specialAccepted && (e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (specialAccepted && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region -- redirect events --

        private void SetEvents()
        {
            textBox.KeyPress += Redirect_KeyPress;
            textBox.KeyDown += Redirect_KeyDown;
            textBox.KeyUp += Redirect_KeyUp;
            textBox.PreviewKeyDown += Redirect_PreviewKeyDown;
            textBox.TextChanged += Redirect_TextChanged;

            textBox.KeyDown += EnterPressedEvent;
        }

        private void Redirect_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }
        private void Redirect_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }
        private void Redirect_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }
        private void Redirect_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }
        private void Redirect_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        public event EventHandler<KeyEventArgs> EnterPressed;
        protected virtual void EnterPressedEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var handler = EnterPressed; // We do not want racing conditions!
                handler?.Invoke(sender, e);
            }
        }

        #endregion
    }
}