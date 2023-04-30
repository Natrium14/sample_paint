using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintPipette
    {
        public PaintPipette() { }

        public void GetColor(int x, int y, ref Bitmap bitmap, ref Color curColor)
        {
            curColor = bitmap.GetPixel(x, y);
        }
    }
}
