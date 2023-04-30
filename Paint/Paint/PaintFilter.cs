using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public abstract class PaintFilter: PaintBase
    {
        protected ProgressBar progressBar;

        protected bool flag1 = true;
        protected bool flag2 = true;
        protected bool flag3 = true;
        protected bool flag4 = true;

        public PaintFilter(PictureBox pictureBox, Bitmap bitmap, ProgressBar progressBar)
            :base(pictureBox, bitmap)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.progressBar = progressBar;
        }

        protected void SetValueProgressBar(int i)
        {
            if (i >= pictureBox.Width / 4 && flag1)
            {
                progressBar.Value = 25;
                flag1 = false;
            }
            if (i >= pictureBox.Width / 2 && flag2)
            {
                progressBar.Value = 50;
                flag2 = false;
            }
            if (i >= (pictureBox.Width / 4 * 3) && flag3)
            {
                progressBar.Value = 75;
                flag3 = false;
            }
            if (i >= pictureBox.Width - 10 && flag4)
            {
                progressBar.Value = 100;
                flag4 = false;
            }
        }
    }
}
