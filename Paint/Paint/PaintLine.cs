using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintLine: PaintBase
    {
        protected Point startPoint;
        protected Point endPoint;
        protected CheckBox checkBox;

        public PaintLine(PictureBox pictureBox, Bitmap bitmap)
            : base(pictureBox, bitmap)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.startPoint = new Point(0, 0);
            this.endPoint = new Point(0, 0);
        }

        public PaintLine(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.graphics = graphics;
            this.startPoint = new Point(0, 0);
            this.endPoint = new Point(0, 0);
        }

        public void SetStartPoint(int x, int y)
        {
            startPoint.X = x;
            startPoint.Y = y;
        }

        public void SetEndPoint(int x, int y)
        {
            endPoint.X = x;
            endPoint.Y = y;
        }

        public void PaintDrawLine(Brush brush, int brushSize)
        {
            Pen pen = new Pen(brush);
            pen.Width = (float)brushSize;
            graphics.DrawLine(pen, startPoint, endPoint);
            pictureBox.Image = bitmap;
        }

        public virtual void PaintDraw() { }

        public virtual void PaintDrawFill() { }
    }
}
