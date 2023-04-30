using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Paint
{
    class PaintFillSmooth : PaintFill
    {
        public PaintFillSmooth(PictureBox pictureBox, Bitmap bitmap)
            : base(pictureBox, bitmap)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
        }

        public void FillSmooth(int x, int y, Color fillColor)
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
                    whitePoints.AddRange(GetNeibPointsSmooth(p));
                points = whitePoints.Except(points).ToList();
                if (whitePoints.Count == 0)
                    flag = false;
                foreach (var p in points)
                    bitmap.SetPixel(p.X, p.Y, fillColor);
                whitePoints.RemoveRange(0, whitePoints.Count);
            } while (flag);

            pictureBox.Image = bitmap;
        }

        private List<Point> GetNeibPointsSmooth(Point curPoint)
        {
            List<Point> points = new List<Point>();
            int dev = 10;

            try
            {
                Point newPoint = new Point(curPoint.X, curPoint.Y - 1);
                if (PointInCanvas(newPoint))
                {
                    var color = this.bitmap.GetPixel(newPoint.X, newPoint.Y);
                    if ((Math.Abs(color.R - baseColor.R) <= dev) &&
                        (Math.Abs(color.G - baseColor.G) <= dev) &&
                        (Math.Abs(color.B - baseColor.B) <= dev))
                        points.Add(newPoint);
                }
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X, curPoint.Y + 1);
                if (PointInCanvas(newPoint))
                {
                    var color = this.bitmap.GetPixel(newPoint.X, newPoint.Y);
                    if ((Math.Abs(color.R - baseColor.R) <= dev) &&
                        (Math.Abs(color.G - baseColor.G) <= dev) &&
                        (Math.Abs(color.B - baseColor.B) <= dev))
                        points.Add(newPoint);
                }
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y);
                if (PointInCanvas(newPoint))
                {
                    var color = this.bitmap.GetPixel(newPoint.X, newPoint.Y);
                    if ((Math.Abs(color.R - baseColor.R) <= dev) &&
                        (Math.Abs(color.G - baseColor.G) <= dev) &&
                        (Math.Abs(color.B - baseColor.B) <= dev))
                        points.Add(newPoint);
                }
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y);
                if (PointInCanvas(newPoint))
                {
                    var color = this.bitmap.GetPixel(newPoint.X, newPoint.Y);
                    if ((Math.Abs(color.R - baseColor.R) <= dev) &&
                        (Math.Abs(color.G - baseColor.G) <= dev) &&
                        (Math.Abs(color.B - baseColor.B) <= dev))
                        points.Add(newPoint);
                }
            }
            catch (Exception ee) { }
            /*
            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y - 1);
                if (PointInCanvas(newPoint))
                {
                    var color = this.bitmap.GetPixel(newPoint.X, newPoint.Y);
                    if ((Math.Abs(color.R - baseColor.R) <= dev) &&
                        (Math.Abs(color.G - baseColor.G) <= dev) &&
                        (Math.Abs(color.B - baseColor.B) <= dev))
                        points.Add(newPoint);
                }
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y - 1);
                if (PointInCanvas(newPoint))
                {
                    var color = this.bitmap.GetPixel(newPoint.X, newPoint.Y);
                    if ((Math.Abs(color.R - baseColor.R) <= dev) &&
                        (Math.Abs(color.G - baseColor.G) <= dev) &&
                        (Math.Abs(color.B - baseColor.B) <= dev))
                        points.Add(newPoint);
                }
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y + 1);
                if (PointInCanvas(newPoint))
                {
                    var color = this.bitmap.GetPixel(newPoint.X, newPoint.Y);
                    if ((Math.Abs(color.R - baseColor.R) <= dev) &&
                        (Math.Abs(color.G - baseColor.G) <= dev) &&
                        (Math.Abs(color.B - baseColor.B) <= dev))
                        points.Add(newPoint);
                }
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y + 1);
                if (PointInCanvas(newPoint))
                {
                    var color = this.bitmap.GetPixel(newPoint.X, newPoint.Y);
                    if ((Math.Abs(color.R - baseColor.R) <= dev) &&
                        (Math.Abs(color.G - baseColor.G) <= dev) &&
                        (Math.Abs(color.B - baseColor.B) <= dev))
                        points.Add(newPoint);
                }
            }
            catch (Exception ee) { }
            */
            return points;
        }
    }
}
