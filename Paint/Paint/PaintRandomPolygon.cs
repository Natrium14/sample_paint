using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintRandomPolygon : PaintLine
    {
        public PaintRandomPolygon(PictureBox pictureBox, Bitmap bitmap, Graphics graphics, CheckBox checkBox)
            : base(pictureBox, bitmap, graphics)
        {
            this.checkBox = checkBox;
        }

        public void PaintDrawrandPoly(Brush brush, int brushSize)
        {
            if (checkBox.Checked)
            {
                if ((endPoint.X > startPoint.X) && (endPoint.Y > startPoint.Y))
                {
                    Random r = new Random();
                    int width = Math.Abs(endPoint.X - startPoint.X);
                    int height = Math.Abs(endPoint.Y - startPoint.Y);

                    int r1x = r.Next(startPoint.X, startPoint.X + width / 2);
                    int r1y = r.Next(startPoint.Y, startPoint.Y + height / 2);
                    Point point1 = new Point(r1x, r1y);
                    int r2x = r.Next(startPoint.X + width / 2, endPoint.X);
                    int r2y = r.Next(startPoint.Y, startPoint.Y + height / 2);
                    Point point2 = new Point(r2x, r2y);
                    int r3x = r.Next(startPoint.X, startPoint.X + width / 2);
                    int r3y = r.Next(startPoint.Y + height / 2, endPoint.Y);
                    Point point3 = new Point(r3x, r3y);
                    int r4x = r.Next(startPoint.X + width / 2, endPoint.X);
                    int r4y = r.Next(startPoint.Y + height / 2, endPoint.Y);
                    Point point4 = new Point(r4x, r4y);

                    Point[] curvePoints = { point1, point2, point4, point3 };
                    graphics.FillPolygon(brush, curvePoints);
                }
                if ((endPoint.X < startPoint.X) && (endPoint.Y < startPoint.Y))
                {
                    Random r = new Random();
                    int width = Math.Abs(endPoint.X - startPoint.X);
                    int height = Math.Abs(endPoint.Y - startPoint.Y);

                    int r1x = r.Next(endPoint.X, endPoint.X + width / 2);
                    int r1y = r.Next(endPoint.Y, endPoint.Y + height / 2);
                    Point point1 = new Point(r1x, r1y);
                    int r2x = r.Next(endPoint.X + width / 2, startPoint.X);
                    int r2y = r.Next(endPoint.Y, endPoint.Y + height / 2);
                    Point point2 = new Point(r2x, r2y);
                    int r3x = r.Next(endPoint.X + width / 2, startPoint.X);
                    int r3y = r.Next(endPoint.Y + height / 2, startPoint.Y);
                    Point point3 = new Point(r3x, r3y);
                    int r4x = r.Next(endPoint.X, endPoint.X + width / 2);
                    int r4y = r.Next(endPoint.Y + height / 2, startPoint.Y);
                    Point point4 = new Point(r4x, r4y);

                    Point[] curvePoints = { point1, point2, point3, point4 };
                    graphics.FillPolygon(brush, curvePoints);
                    //graphics.FillRectangle(brush, endPoint.X, endPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
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
