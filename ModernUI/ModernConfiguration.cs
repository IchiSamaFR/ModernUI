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
    public static class ModernConfiguration
    {
        public static Color disabled_color;
        public static Color inactive_color;
        public static Color back_color;
        public static Color main_color;
        public static Color secondary_color;
        public static Color titlefont_color;
        public static Color font_color;
        public static int AlphaValue = 120;

        private static bool set = false;

        public static void SetConfiguration()
        {
            if (set)
                return;
            inactive_color = Color.FromArgb(200, 200, 200);
            disabled_color = Color.FromArgb(220, 220, 220);
            main_color = Color.FromArgb(0, 148, 255);
            secondary_color = Color.FromArgb(92, 225, 230);
            back_color = Color.FromArgb(250, 250, 250);
            titlefont_color = Color.White;
            font_color = Color.Black;
            set = true;
        }
    }
}
