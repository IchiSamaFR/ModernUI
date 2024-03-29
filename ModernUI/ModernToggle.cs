﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ModernUI;

namespace ModernUI.Assets
{
    public class ModernToggle : ModernDesigner
    {
        private Font fnt = new Font("Sans Serif", 10);
        private int titleHeight = 30;
        private int dotSize = 12;
        private Color dotColor;
        private Color dotActiveColor;
        private Color fillColor;
        private Color fillActiveColor;
        private int titlePadding = 4;
        private Size mainSize;

        [System.ComponentModel.Category("Modern Toggle")]
        public int DotSize
        {
            get { return dotSize; }
            set
            {
                if (dotSize == value)
                    return;
                dotSize = value;
                Refresh();

            }
        }
        [System.ComponentModel.Category("Modern Toggle")]
        public Color DotColor
        {
            get { return dotColor; }
            set
            {
                if (dotColor == value)
                    return;
                dotColor = value;
                Refresh();
            }
        }
        [System.ComponentModel.Category("Modern Toggle")]
        public Color DotActiveColor
        {
            get { return dotActiveColor; }
            set
            {
                if (dotActiveColor == value)
                    return;
                dotActiveColor = value;
                Refresh();
            }
        }
        [System.ComponentModel.Category("Modern Toggle")]
        public Color FillActiveColor
        {
            get { return fillActiveColor; }
            set
            {
                if (fillActiveColor == value)
                    return;
                fillActiveColor = value;
                Refresh();
            }
        }
        [System.ComponentModel.Category("Modern Toggle")]
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
        [System.ComponentModel.Category("Modern Toggle")]
        public int TitleHeight
        {
            get { return titleHeight; }
            set
            {
                if (titleHeight == value)
                    return;
                titleHeight = value;
                Refresh();
            }
        }
        [System.ComponentModel.Category("Modern Toggle")]
        public bool Checked
        {
            get { return isChecked; }
            set
            {
                if (isChecked == value)
                    return;
                isChecked = value;
                Refresh();
            }
        }
        bool isChecked = false;


        public ModernToggle() : base()
        {
            this.MouseClick += OnClick;

            dotColor = Color.White;
            dotActiveColor = Color.White;
            fillColor = ModernConfiguration.inactive_color;
            fillActiveColor = ModernConfiguration.main_color;
            mainSize = new Size(36, 20);
            AutoSize = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var path = new GraphicsPath();

            if (isChecked)
            {
                if (Enabled && ParentEnabled)
                {
                    CreateRecObject(e, path, mainSize.Width - mainSize.Height + (mainSize.Height - dotSize) / 2, (mainSize.Height - dotSize) / 2, dotSize, dotSize, dotActiveColor);
                    CreateRecObject(e, path, 0, 0, mainSize.Width, mainSize.Height, fillActiveColor);
                }
                else
                {
                    CreateRecObject(e, path, mainSize.Width - mainSize.Height + (mainSize.Height - dotSize) / 2, (mainSize.Height - dotSize) / 2, dotSize, dotSize,
                        Color.FromArgb(ModernConfiguration.AlphaValue, dotActiveColor.R, dotActiveColor.G, dotActiveColor.B));
                    CreateRecObject(e, path, 0, 0, mainSize.Width, mainSize.Height,
                        Color.FromArgb(ModernConfiguration.AlphaValue, fillActiveColor.R, fillActiveColor.G, fillActiveColor.B));
                }
            }
            else
            {
                if (Enabled && ParentEnabled)
                {
                    CreateRecObject(e, path, (mainSize.Height - dotSize) / 2, (mainSize.Height - dotSize) / 2, dotSize, dotSize, dotColor);
                    CreateRecObject(e, path, 0, 0, mainSize.Width, mainSize.Height, fillColor);
                }
                else
                {
                    CreateRecObject(e, path, (mainSize.Height - dotSize) / 2, (mainSize.Height - dotSize) / 2, dotSize, dotSize,
                        Color.FromArgb(ModernConfiguration.AlphaValue, dotColor.R, dotColor.G, dotColor.B));
                    CreateRecObject(e, path, 0, 0, mainSize.Width, mainSize.Height,
                        Color.FromArgb(ModernConfiguration.AlphaValue, fillColor.R, fillColor.G, fillColor.B));
                }
            }

            using (Brush aBrush = new SolidBrush(FontColor))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;

                SizeF stringSizeF = e.Graphics.MeasureString(Text, TextFont);
                Size stringSize = new Size((int)Math.Ceiling(stringSizeF.Width), (int)Math.Ceiling(stringSizeF.Height));

                Rectangle rec = new Rectangle(titlePadding + mainSize.Width,
                                                mainSize.Height - stringSize.Height,
                                                stringSize.Width,
                                                stringSize.Height);
                e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                Size = new Size(rec.X + rec.Width, mainSize.Height + 1);
            }

            path.Dispose();
        }

        public override void OnClick(object sender, EventArgs e)
        {
            if (!Enabled || !ParentEnabled)
                return;
            if (isHover)
            {
                isChecked = !isChecked;
                CheckedChangedEvent(sender, e);
            }
            RefreshAll();
        }


        public event EventHandler<EventArgs> CheckedChanged;
        protected virtual void CheckedChangedEvent(object sender, EventArgs e)
        {
            var handler = CheckedChanged; // We do not want racing conditions!
            handler?.Invoke(sender, e);
        }
    }
}
