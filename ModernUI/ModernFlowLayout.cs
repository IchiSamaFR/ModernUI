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
        
        [System.ComponentModel.Category("Modern Flow Layout")]
        public int Columns
        {
            get { return colRows.X; }
            set
            {
                colRows.X = value;
                Refresh();
            }
        }
        [System.ComponentModel.Category("Modern Flow Layout")]
        public int Rows
        {
            get { return colRows.Y; }
            set
            {
                colRows.Y = value;
                Refresh();
            }
        }
        [System.ComponentModel.Category("Modern Flow Layout")]
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
        [System.ComponentModel.Category("Modern Flow Layout")]
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
            DoubleBuffered = false;
            AutoScroll = true;
            BorderWidth = 0;
            Size = new Size(150, 200);
            BorderColor = FillColor;
            FillColor = Color.White;
            Margin = new Padding(5, 5, 5, 5);
            Padding = new Padding(5, 5, 5, 5);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            RefreshChilds();
            PerformLayout();
            base.OnPaint(e);
        }
        
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }

        private void RefreshChilds()
        {
            int actualRow = 0;
            int actualCol = 0;
            
            for (int index = 0; index < Controls.Count; index++)
            {
                Control item = Controls[index];

                if (TopToDown)
                {
                    if(colRows.X == 0)
                    {
                        actualCol = 0;
                    }
                    else if (actualCol == colRows.X)
                    {
                        actualCol = 0;
                        actualRow++;
                    }
                    else if (actualRow == colRows.Y)
                    {
                        actualRow = 0;
                    }
                }
                else
                {
                    if (actualRow == colRows.Y && colRows.Y != 0)
                    {
                        actualRow = 0;
                        actualCol++;
                    }
                    else if (actualCol == colRows.X)
                    {
                        actualCol = 0;
                    }
                }
                if ((topToDown && actualRow >= colRows.Y && colRows.Y != 0 && colRows.X != 0)
                    || (!topToDown && colRows.Y != 0 && actualCol >= colRows.X && colRows.X != 0))
                {
                    Controls[index].Visible = false;
                    continue;
                }
                else
                {
                    Controls[index].Visible = true;
                }

                if (index > 0)
                {
                    item.Location = new Point((Controls[index - 1].Width + Padding.Right) * actualCol - HorizontalScroll.Value,
                                              (Controls[index - 1].Height + Padding.Top) * actualRow - VerticalScroll.Value);
                }
                else
                {
                    item.Location = new Point(-HorizontalScroll.Value, -VerticalScroll.Value);
                }
                if (colRows.X != 0)
                {
                    actualCol++;
                }
                else
                {
                    actualRow++;
                }
            }
        }
    }
}