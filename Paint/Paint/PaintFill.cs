using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintFill: PaintBase
    {
        protected Color baseColor;

        public PaintFill(PictureBox pictureBox, Bitmap bitmap)
            : base(pictureBox, bitmap)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
        }

        protected bool PointInCanvas(Point curPoint)
        {
            if (curPoint.X > 0 && curPoint.X < bitmap.Width && curPoint.Y > 0 && curPoint.Y < bitmap.Width)
                return true;
            else return false;
        }

        public void Fill(int x, int y, Color fillColor)
        {
            this.baseColor = bitmap.GetPixel(x, y);

            List<Point> points = new List<Point>();
            points.Add(new Point(x, y));

            List<Point> whitePoints = new List<Point>();
            whitePoints.Add(new Point(x, y));
            bool flag = true;

            do
            {
                foreach (var p in points)
                {
                    whitePoints.AddRange(GetNeibPoints(p));
                }

                points = whitePoints.Except(points).ToList();

                if (whitePoints.Count == 0)
                {
                    flag = false;
                }

                foreach (var p in points)
                {
                    bitmap.SetPixel(p.X, p.Y, fillColor);
                }

                whitePoints.RemoveRange(0, whitePoints.Count);
                
            } while (flag); // пока не закончатся "соседи"

            pictureBox.Image = bitmap;
        }

        private List<Point> GetNeibPoints(Point curPoint)
        {
            List<Point> points = new List<Point>();

            try
            {
                Point newPoint = new Point(curPoint.X, curPoint.Y - 1);
                if (PointInCanvas(newPoint))
                    if (this.bitmap.GetPixel(newPoint.X, newPoint.Y) == baseColor)
                        points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X, curPoint.Y + 1);
                if (PointInCanvas(newPoint))
                    if (this.bitmap.GetPixel(newPoint.X, newPoint.Y) == baseColor)
                        points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y);
                if (PointInCanvas(newPoint))
                    if (this.bitmap.GetPixel(newPoint.X, newPoint.Y) == baseColor)
                        points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y);
                if (PointInCanvas(newPoint))
                    if (this.bitmap.GetPixel(newPoint.X, newPoint.Y) == baseColor)
                        points.Add(newPoint);
            }
            catch (Exception ee) { }
            /* проверять точки наискосок не нужно
            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y - 1);
                if (this.bitmap.GetPixel(newPoint.X, newPoint.Y) == baseColor)
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y - 1);
                if (this.bitmap.GetPixel(newPoint.X, newPoint.Y) == baseColor)
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y + 1);
                if (this.bitmap.GetPixel(newPoint.X, newPoint.Y) == baseColor)
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y + 1);
                if (this.bitmap.GetPixel(newPoint.X, newPoint.Y) == baseColor)
                    points.Add(newPoint);
            }
            catch (Exception ee) { }
            */
            return points;
        }


    }
}
