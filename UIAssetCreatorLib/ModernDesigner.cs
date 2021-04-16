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
    public class ModernDesigner : UserControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams CP = base.CreateParams;
                CP.ExStyle = CP.ExStyle | 0x02000000; // WS_EX_COMPOSITED
                return CP;
            }
        }

        bool enabled;
        string text = "label";
        private Font fnt = new Font("Sans Serif", 10);
        private int borderRadius = 20;
        private int borderWidth = 2;
        private Color borderColor;
        private Color fillColor;
        private string textAlign = "center";
        private int textPadding = 10;
        private Color fontColor;

        public new bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                Refresh();
            }
        }
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        public override string Text
        {
            get { return text; }
            set
            {
                if (text == value)
                    return;
                text = value;
                RefreshType("text");
            }
        }
        public Font TextFont
        {
            get { return fnt; }
            set
            {
                if (fnt == value)
                    return;
                fnt = value;

            }
        }
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                if (borderRadius == value)
                    return;
                borderRadius = value;
                Refresh();
            }
        }
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                if (borderRadius == value)
                    return;
                borderWidth = value;
                Refresh();

            }
        }
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor == value)
                    return;
                borderColor = value;
                Refresh();

            }
        }
        public Color FillColor
        {
            get { return fillColor; }
            set
            {
                if (fillColor == value)
                    return;
                fillColor = value;
                Refresh();
            }
        }
        public Color FontColor
        {
            get { return fontColor; }
            set
            {
                if (fontColor == value)
                    return;
                fontColor = value;
                Refresh();
            }
        }
        public string TextAlign
        {
            get { return textAlign; }
            set
            {
                if (textAlign == value)
                    return;
                textAlign = value;
                Refresh();
            }
        }
        public int TextPadding
        {
            get { return textPadding; }
            set
            {
                if (textPadding == value)
                    return;
                textPadding = value;
                Refresh();
            }
        }
        public bool ParentEnabled
        {
            get
            {
                ModernDesigner design;
                if (Parent != null && enabled)
                {
                    if (Parent is Form)
                    {
                        return true;
                    }
                    else if (!Parent.Enabled)
                    {
                        return Parent.Enabled;
                    }
                    else if (Parent is ModernDesigner)
                    {
                        design = Parent as ModernDesigner;
                        bool ret = design.ParentEnabled;
                        return ret;
                    }
                    else if (!(Parent is ModernDesigner))
                    {
                        bool ret = Parent.Enabled;
                        return ret;
                    }
                }
                return enabled;
            }
        }

        public bool isHover = false;
        public bool isDown = false;

        public ModernDesigner()
        {
            ModernConfiguration.SetConfiguration();


            this.SizeChanged += Refresh;
            this.EnabledChanged += Refresh;

            this.MouseEnter += OnEnter;
            this.MouseDown += OnDown;
            this.MouseUp += OnUp;
            this.MouseLeave += OnLeave;

            borderColor = ModernConfiguration.secondary_color;
            fillColor = ModernConfiguration.main_color;
            BackColor = Color.Transparent;
            fontColor = ModernConfiguration.titlefont_color;
            Size = new Size(150, 40);
            enabled = true;
            text = "Modern Design";
        }
        public void CreateRecObject(PaintEventArgs e, GraphicsPath path, int x, int y, int width, int height, Color color)
        {
            if (height <= 1 || width <= 1)
            {
                return;
            }

            int borderR = GetBorderMax(borderRadius, width, height);
            RectangleF circleRect = new RectangleF(x, y, borderR, borderR);
            RectangleF circleRect2 = new RectangleF(x + width - (borderR + 1), y, borderR, borderR);
            RectangleF circleRect3 = new RectangleF(x + width - (borderR + 1), y + height - (borderR + 1), borderR, borderR);
            RectangleF circleRect4 = new RectangleF(x, y + height - (borderR + 1), borderR, borderR);

            if (borderR > 0)
            {
                path.AddArc(circleRect, 180, 90);
                path.AddArc(circleRect2, 270, 90);
                path.AddArc(circleRect3, 0, 90);
                path.AddArc(circleRect4, 90, 90);
            }
            else
            {
                path.AddLine(new Point(x, y),
                             new Point(width - x, y));

                path.AddLine(new Point(width, y),
                             new Point(width, height - y));

                path.AddLine(new Point(width - x + 1, height),
                             new Point(x, height));

                path.AddLine(new Point(x, height - y),
                             new Point(x, y));
            }
            path.CloseFigure();

            Brush _fillColor = new SolidBrush(color);
            //Pen _borderColor = new Pen(borderColor);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.FillPath(_fillColor, path);
            //e.Graphics.DrawPath(_borderColor, path);
        }
        public void CreateRecObject_DownBorder(PaintEventArgs e, GraphicsPath path, int x, int y, int width, int height, Color color)
        {
            if (height <= 1 || width <= 1)
            {
                return;
            }

            int borderR = GetBorderMax(borderRadius, width, height);
            RectangleF circleRect3 = new RectangleF(width - borderR - 1, y + height - (borderR + 1), borderR, borderR);
            RectangleF circleRect4 = new RectangleF(x, y + height - (borderR + 1), borderR, borderR);

            if (borderR > 0)
            {
                path.AddArc(circleRect3, 0, 90);

                path.AddArc(circleRect4, 90, 90);

                path.AddLine(new Point(x, y),
                             new Point(width - 1, y));
            }
            else
            {
                path.AddLine(new Point(x, y),
                             new Point(width, y));

                path.AddLine(new Point(width, y),
                             new Point(width, height));

                path.AddLine(new Point(width, height),
                             new Point(x, height));

                path.AddLine(new Point(x, height),
                             new Point(x, y));
            }

            path.CloseFigure();

            Brush _fillColor = new SolidBrush(color);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.FillPath(_fillColor, path);
            //e.Graphics.DrawPath(_borderColor, path);
        }

        public int GetBorderMax(int border, int width, int height)
        {
            if (border > height && border > width)
            {
                if (height < width)
                {
                    border = height;
                }
                else
                {
                    border = width;
                }
            }
            else if (border > height)
            {
                border = height;
            }
            else if (border > width)
            {
                border = width;
            }
            return border;
        }

        public virtual void OnEnter(object sender, EventArgs e)
        {
            isHover = true;
        }
        public virtual void OnLeave(object sender, EventArgs e)
        {
            isHover = false;
        }
        public virtual void OnDown(object sender, EventArgs e)
        {
            isDown = true;
        }
        public virtual void OnUp(object sender, EventArgs e)
        {
            isDown = false;
        }
        public virtual void OnClick(object sender, EventArgs e)
        {

        }


        public virtual void RefreshType(string type = "design")
        {

        }
        public void Refresh(object sender, EventArgs e)
        {
            RefreshAll();
        }

        public void RefreshAll()
        {
            Refresh();
        }
    }
}
