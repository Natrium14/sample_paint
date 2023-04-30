using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintBalon: PaintBase
    {
        private CheckBox checkBox;
        public int Strength { get; set; }

        public PaintBalon(PictureBox pictureBox, Bitmap bitmap, CheckBox checkBox, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {
            this.bitmap = bitmap;
            this.graphics = graphics;
            this.pictureBox = pictureBox;
            this.checkBox = checkBox;
            this.Strength = 5;
        }

        public void Fill(int x, int y, Color fillColor, int brushSize)
        {
            int xSize = x - brushSize / 2;
            int ySize = y - brushSize / 2;
            Random r = new Random();
            var selectionBounds = graphics.VisibleClipBounds;

            try // вместо трай написать условия проверки на край холста
            {
                for (int i = xSize; i < (xSize + brushSize); i++)
                    for (int j = ySize; j < (ySize + brushSize); j++)
                    {
                        if (this.checkBox.Checked)
                        {
                            if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                            {
                                if (Math.Pow(i - x, 2) + Math.Pow(j - y, 2) <= Math.Pow(brushSize / 2, 2))
                                {
                                    if (r.Next(0, Strength) == 0)
                                    {
                                        bitmap.SetPixel(i, j, fillColor);
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                            {
                                if (r.Next(0, Strength) == 0)
                                {
                                    bitmap.SetPixel(i, j, fillColor);
                                }
                            }
                        }
                    }

                pictureBox.Image = bitmap;
            }
            catch (Exception ee) { }
        }        


        /*
        public void FillParallel(int x, int y, Color fillColor, int brushSize)
        {
            int xSize = x - brushSize / 2;
            int ySize = y - brushSize / 2;
            Random r = new Random();
            List<Point> points = new List<Point>();
            Object lockMe = new Object();  

            try // вместо трай написать условия проверки на край холста
            {
                Parallel.For(xSize, (xSize + brushSize), i => 
                {
                    Parallel.For(ySize, ySize + brushSize, j =>
                    {
                        if (this.checkBox.Checked)
                        {
                            if (Math.Pow(i - x, 2) + Math.Pow(j - y, 2) <= Math.Pow(brushSize / 2, 2))
                            {
                                if (r.Next(0, Strength) == 0)
                                {
                                    Point newPoint = new Point(i, j);
                                    lock (lockMe)
                                    {
                                        points.Add(newPoint);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (r.Next(0, Strength) == 0)
                            {
                                Point newPoint = new Point(i, j);
                                lock (lockMe)
                                {
                                    points.Add(newPoint);
                                }
                            }
                        }
                    });
                });

                foreach (var p in points)
                {
                    bitmap.SetPixel(p.X, p.Y, fillColor);
                }

                pictureBox.Image = bitmap;
            }
            catch (Exception ee) { }
        }*/
    }
}
