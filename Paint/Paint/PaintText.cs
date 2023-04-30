using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintText: PaintBase
    {

        public PaintText(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.graphics = graphics;
        }

        public void SetText(int x, int y, Brush brush)
        {
            FormPaintText textForm = new FormPaintText();
            var result = textForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                var font = textForm.font;
                graphics.DrawString(textForm.textBox1.Text, font, brush, new Point(x, y));
            }
            textForm.Dispose();

            pictureBox.Image = bitmap;
        }
    }
}
