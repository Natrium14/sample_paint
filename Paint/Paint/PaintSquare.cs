using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintSquare : PaintLine
    {
        public PaintSquare(PictureBox pictureBox, Bitmap bitmap, Graphics graphics, CheckBox checkBox)
            : base(pictureBox, bitmap, graphics)
        {
            this.checkBox = checkBox;
        }

        public void PaintDrawSquare(Brush brush, int brushSize)
        {
            if (checkBox.Checked)
            {
                if ((endPoint.X > startPoint.X) && (endPoint.Y > startPoint.Y))
                {
                    graphics.FillRectangle(brush, startPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                }
                if ((endPoint.X < startPoint.X) && (endPoint.Y < startPoint.Y))
                {
                    graphics.FillRectangle(brush, endPoint.X, endPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                }
                if ((endPoint.X > startPoint.X) && (endPoint.Y < startPoint.Y))
                {
                    graphics.FillRectangle(brush, startPoint.X, endPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                }
                if ((endPoint.X < startPoint.X) && (endPoint.Y > startPoint.Y))
                {
                    graphics.FillRectangle(brush, endPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                }
            }
            else
            {
                Pen pen = new Pen(brush);
                pen.Width = (float)brushSize;

                if ((endPoint.X > startPoint.X) && (endPoint.Y > startPoint.Y))
                {
                    graphics.DrawRectangle(pen, startPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                }
                if ((endPoint.X < startPoint.X) && (endPoint.Y < startPoint.Y))
                {
                    graphics.DrawRectangle(pen, endPoint.X, endPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                }
                if ((endPoint.X > startPoint.X) && (endPoint.Y < startPoint.Y))
                {
                    graphics.DrawRectangle(pen, startPoint.X, endPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                }
                if ((endPoint.X < startPoint.X) && (endPoint.Y > startPoint.Y))
                {
                    graphics.DrawRectangle(pen, endPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                }
            }

            pictureBox.Image = bitmap;
        }
    }
}
