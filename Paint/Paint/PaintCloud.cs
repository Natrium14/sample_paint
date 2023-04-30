using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintCloud
    {
        private Bitmap bitmap;
        private PictureBox pictureBox;
        private Random r;

        public PaintCloud(PictureBox pictureBox, Bitmap bitmap)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            r = new Random();
        }

        public void DrawLine(int x, int y)
        {
            /*
            for (int i = 0; i < 10; i++)
            {
                switch (r.Next(0, 4))
                {
                    case 0:
                        DrawRightLine(x, y);
                        break;
                    case 1:
                        DrawLeftLine(x, y);
                        break;
                    case 2:
                        DrawUpLine(x, y);
                        break;
                    case 3:
                        DrawDownLine(x, y);
                        break;
                }
            }*/

            DrawRightLine(x, y);
        }

        public void DrawRightLine(int x, int y)
        {            
            int n = 100;
            List<Point> points = new List<Point>();

            for (int i = x; i < x + n; i++)
            {
                try
                {
                    int aa = r.Next(-1, 2);
                    y = y + aa;
                    bitmap.SetPixel(i, y, Color.FromArgb(80, 80, 80));
                    points.Add(new Point(i, y));
                }
                catch (Exception ee) { break; }
            }

            List<Point> neibPoints = new List<Point>();
            int colorD = 50;
            for (int i = 0; i < 50; i++)
            {
                foreach (var p in points)
                {
                    neibPoints.AddRange(GetNeibPoints(p));
                }

                neibPoints = neibPoints.Except(points).ToList();

                foreach (var p in neibPoints)
                {
                    try
                    {
                        int aa = r.Next(0, 2);
                        if (aa == 0)
                        {
                            var c = bitmap.GetPixel(p.X, p.Y);
                            bitmap.SetPixel(p.X, p.Y, Color.FromArgb(c.R ^ colorD, c.G ^ colorD, c.B ^ colorD));
                        }
                    }
                    catch (Exception ee) { }
                }

                colorD += 1;
                Console.WriteLine(colorD);
                points = new List<Point>();
                points.AddRange(neibPoints);
            }

            pictureBox.Image = bitmap;
        }

        public void DrawLeftLine(int x, int y)
        {
            int n = 100;
            List<Point> points = new List<Point>();

            for (int i = x; i > x - n; i--)
            {
                try
                {
                    int aa = r.Next(-1, 2);
                    y = y + aa;
                    bitmap.SetPixel(i, y, Color.Black);
                    points.Add(new Point(i, y));
                }
                catch (Exception ee) { break; }
            }

            List<Point> neibPoints = new List<Point>();
            int colorD = 150;
            for (int i = 0; i < 50; i++)
            {
                foreach (var p in points)
                {
                    neibPoints.AddRange(GetNeibPoints(p));
                }

                neibPoints = neibPoints.Except(points).ToList();

                foreach (var p in neibPoints)
                {
                    try
                    {
                        int aa = r.Next(0, 4);
                        if (aa == 0)
                        {
                            bitmap.SetPixel(p.X, p.Y, Color.FromArgb(colorD, colorD, colorD));
                        }
                    }
                    catch (Exception ee) { }
                }

                colorD += 2;

                points = new List<Point>();
                points.AddRange(neibPoints);
            }

            pictureBox.Image = bitmap;
        }

        public void DrawUpLine(int x, int y)
        {
            int n = 100;
            List<Point> points = new List<Point>();

            for (int j = y; j > y - n; j--)
            {
                try
                {
                    int aa = r.Next(-1, 2);
                    x = x + aa;
                    bitmap.SetPixel(x, j, Color.Black);
                    points.Add(new Point(x, j));
                }
                catch (Exception ee) { break; }
            }

            List<Point> neibPoints = new List<Point>();
            int colorD = 150;
            for (int i = 0; i < 50; i++)
            {
                foreach (var p in points)
                {
                    neibPoints.AddRange(GetNeibPoints(p));
                }

                neibPoints = neibPoints.Except(points).ToList();

                foreach (var p in neibPoints)
                {
                    try
                    {
                        int aa = r.Next(0, 4);
                        if (aa == 0)
                        {
                            bitmap.SetPixel(p.X, p.Y, Color.FromArgb(colorD, colorD, colorD));
                        }
                    }
                    catch (Exception ee) { }
                }

                colorD += 2;

                points = new List<Point>();
                points.AddRange(neibPoints);
            }

            pictureBox.Image = bitmap;
        }

        public void DrawDownLine(int x, int y)
        {
            int n = 100;
            List<Point> points = new List<Point>();

            for (int j = y; j < y + n; j++)
            {
                try
                {
                    int aa = r.Next(-1, 2);
                    x = x + aa;
                    bitmap.SetPixel(x, j, Color.Black);
                    points.Add(new Point(x, j));
                }
                catch (Exception ee) { break; }
            }

            List<Point> neibPoints = new List<Point>();
            int colorD = 150;
            for (int i = 0; i < 50; i++)
            {
                foreach (var p in points)
                {
                    neibPoints.AddRange(GetNeibPoints(p));
                }

                neibPoints = neibPoints.Except(points).ToList();

                foreach (var p in neibPoints)
                {
                    try
                    {
                        int aa = r.Next(0, 4);
                        if (aa == 0)
                        {
                            bitmap.SetPixel(p.X, p.Y, Color.FromArgb(colorD, colorD, colorD));
                        }
                    }
                    catch (Exception ee) { }
                }

                colorD += 2;

                points = new List<Point>();
                points.AddRange(neibPoints);
            }

            pictureBox.Image = bitmap;
        }

        private bool PointInCanvas(Point curPoint)
        {
            if (curPoint.X > 0 && curPoint.X < bitmap.Width && curPoint.Y > 0 && curPoint.Y < bitmap.Width)
                return true;
            else return false;
        }

        private List<Point> GetNeibPoints(Point curPoint)
        {
            List<Point> points = new List<Point>();
            
            try
            {
                Point newPoint = new Point(curPoint.X, curPoint.Y - 1);
                if (PointInCanvas(newPoint))
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X, curPoint.Y + 1);
                if (PointInCanvas(newPoint))
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y);
                if (PointInCanvas(newPoint))
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y);
                if (PointInCanvas(newPoint))
                    points.Add(newPoint);
            }
            catch (Exception ee) { }
            
            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y - 1);
                if (PointInCanvas(newPoint))
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y - 1);
                if (PointInCanvas(newPoint))
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X - 1, curPoint.Y + 1);
                if (PointInCanvas(newPoint))
                    points.Add(newPoint);
            }
            catch (Exception ee) { }

            try
            {
                Point newPoint = new Point(curPoint.X + 1, curPoint.Y + 1);
                if (PointInCanvas(newPoint))
                    points.Add(newPoint);
            }
            catch (Exception ee) { }
            
            return points;
        }
    }
}
