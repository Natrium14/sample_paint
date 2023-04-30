using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public abstract class PaintBase
    {
        protected Bitmap bitmap;
        protected PictureBox pictureBox;
        protected Graphics graphics;

        public PaintBase() 
        {
 
        }

        public PaintBase(PictureBox pictureBox, Bitmap bitmap)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
        }

        public PaintBase(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.graphics = graphics;
        }


    }
}
