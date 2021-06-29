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

using ModernUI;

namespace ModernUI.Assets
{
    public class ModernRadioButton : ModernDesigner
    {
        private Font fnt = new Font("Sans Serif", 10);
        private int dotSize = 10;
        private Color dotColor;
        private Color dotActiveColor;
        private Color fillColor;
        private Color fillActiveColor;
        private Color fontColor;
        private Size mainSize;
        private bool multipleSelection = false;
        private string selectionGroup = "";
        private string toRefresh = "";
        private bool maximizeSize = false;

        [System.ComponentModel.Category("Modern Radio Button")]
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
        [System.ComponentModel.Category("Modern Radio Button")]
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
        [System.ComponentModel.Category("Modern Radio Button")]
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
        [System.ComponentModel.Category("Modern Radio Button")]
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
        [System.ComponentModel.Category("Modern Radio Button")]
        public bool Checked
        {
            get { return isChecked; }
            set
            {
                if (isChecked == value)
                    return;
                isChecked = value;
                Refresh();
                CheckValues();
            }
        }
        [System.ComponentModel.Category("Modern Radio Button")]
        public bool MultipleSelection
        {
            get { return multipleSelection; }
            set
            {
                if (multipleSelection == value)
                    return;
                multipleSelection = value;
                Refresh();
            }
        }
        [System.ComponentModel.Category("Modern Radio Button")]
        public string SelectionGroup
        {
            get { return selectionGroup; }
            set
            {
                if (selectionGroup == value)
                    return;
                selectionGroup = value;
                Refresh();
            }
        }
        [System.ComponentModel.Category("Modern Radio Button")]
        public bool MaximizeSize
        {
            get { return maximizeSize; }
            set
            {
                if (maximizeSize == value)
                    return;
                maximizeSize = value;
                Refresh();
            }
        }

        bool isChecked = false;


        public ModernRadioButton() : base()
        {
            this.MouseClick += OnClick;

            dotColor = Color.White;
            dotActiveColor = Color.White;
            fillColor = ModernConfiguration.inactive_color;
            fillActiveColor = ModernConfiguration.main_color;
            BackColor = Color.Transparent;
            fontColor = ModernConfiguration.font_color;
            mainSize = new Size(16, 16);
            Enabled = true;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            var path = new GraphicsPath();

            if(toRefresh == "design" || toRefresh == "")
            {
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
            }

            if(toRefresh == "text" || toRefresh == "")
            {
                using (Brush aBrush = new SolidBrush(fontColor))
                {
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Near;

                    SizeF stringSizeF = e.Graphics.MeasureString(Text, TextFont);
                    Size stringSize = new Size((int)Math.Ceiling(stringSizeF.Width), (int)Math.Ceiling(stringSizeF.Height));

                    Rectangle rec = new Rectangle(TextPadding + mainSize.Width,
                                                    mainSize.Height - stringSize.Height,
                                                    stringSize.Width,
                                                    stringSize.Height);
                    e.Graphics.DrawString(Text, TextFont, aBrush, rec, format);
                    if (maximizeSize && rec.X + rec.Width < Parent.Width - 26)
                    {
                        Size = new Size(Parent.Width - 26, mainSize.Height + 1);
                    }
                    else
                    {
                        Size = new Size(rec.X + rec.Width, mainSize.Height + 1);
                    }
                }
            }

            path.Dispose();

            toRefresh = "";
        }

        public override void OnClick(object sender, EventArgs e)
        {
            if (!Enabled || !ParentEnabled)
                return;
            if (isHover)
            {
                if (multipleSelection)
                {
                    isChecked = !isChecked;
                    CheckedChangedEvent(sender, e);
                }
                else if (!isChecked)
                {
                    isChecked = true;
                    CheckValues();
                    CheckedChangedEvent(sender, e);
                }
            }
            RefreshAll();
        }
        public override void RefreshType(string type = "design")
        {
            toRefresh = "";
            Refresh();
        }
        
        private void CheckValues()
        {
            if (!isChecked || Parent == null || Parent.Controls.Count <= 0)
                return;
            foreach (var item in this.Parent.Controls)
            {
                ModernRadioButton _item;
                if (item is ModernRadioButton && (_item = item as ModernRadioButton).isChecked && !_item.multipleSelection && _item != this)
                {
                    if (selectionGroup == _item.selectionGroup)
                    {
                        _item.Checked = false;
                    }
                }
            }
        }

        
        public event EventHandler<EventArgs> CheckedChanged;
        protected virtual void CheckedChangedEvent(object sender, EventArgs e)
        {
            var handler = CheckedChanged; // We do not want racing conditions!
            handler?.Invoke(sender, e);
        }
    }
}
