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


namespace ModernUI.Assets
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class ModernFlowLayout : ModernDesigner
    {
        private Point colRows = new Point(0, 2);
        private bool topToDown = true;
        private bool leftToRight = false;

        [Description(""), Category("Modern Flow Layout")]
        public Point ColumnsRows
        {
            get { return colRows; }
            set
            {
                colRows = value;
                Refresh();
            }
        }
        [Description(""), Category("Modern Flow Layout")]
        public bool TopToDown
        {
            get { return topToDown; }
            set
            {
                topToDown = value;
                leftToRight = !value;
                Refresh();
            }
        }
        [Description(""), Category("Modern Flow Layout")]
        public bool LeftToRight
        {
            get { return leftToRight; }
            set
            {
                leftToRight = value;
                TopToDown = !value;
                Refresh();
            }
        }

        public ModernFlowLayout() : base()
        {
            AutoScroll = true;
            BorderWidth = 0;
            Size = new Size(150, 200);
            BorderColor = FillColor;
            FillColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            RefreshChilds();
            PerformLayout();
        }
        
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }

        private void RefreshChilds()
        {
            int actualRow = 0;
            int actualCol = 0;
            int index = 0;

            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Control item = Controls[i];

                if (TopToDown)
                {
                    if (actualCol == colRows.X)
                    {
                        actualCol = 0;
                        actualRow++;
                    }
                }
                else
                {
                    if (actualRow == colRows.Y)
                    {
                        actualRow = 0;
                        actualCol++;
                    }
                }
                if ((topToDown && actualRow >= colRows.Y && colRows.Y != 0 && colRows.X != 0)
                    || (!topToDown && colRows.Y != 0 && actualCol >= colRows.X && colRows.X != 0))
                {
                    Controls[i].Visible = false;
                    continue;
                }
                else
                {
                    Controls[i].Visible = true;
                }

                if (index > 0)
                {
                    item.Location = new Point((Controls[index - 1].Width + Padding.Right) * actualCol - HorizontalScroll.Value,
                                              (Controls[index - 1].Height + Padding.Top) * actualRow - VerticalScroll.Value);
                }
                else
                {
                    item.Location = new Point();
                }
                if (TopToDown)
                {
                    actualCol++;
                }
                else
                {
                    actualRow++;
                }
                index++;
            }
        }
    }
}