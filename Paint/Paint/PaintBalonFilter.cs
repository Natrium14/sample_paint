using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintBalonFilter : PaintBalon
    {
        private CheckBox checkBox;

        public PaintBalonFilter(PictureBox pictureBox, Bitmap bitmap, CheckBox checkBox, Graphics graphics)
            : base(pictureBox, bitmap, checkBox, graphics)
        {
            this.bitmap = bitmap;
            this.graphics = graphics;
            this.pictureBox = pictureBox;
            this.checkBox = checkBox;
        }


        public void FillNoiseColor(int x, int y, Color fillColor, int brushSize)
        {
            int xSize = x - brushSize / 2;
            int ySize = y - brushSize / 2;
            Random rand = new Random();
            int dif = 0, r = 0, g = 0, b = 0;
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
                                    if (rand.Next(0, Strength) == 0)
                                    {
                                        DrawNoiseColor(i, j, fillColor, r, g, b, dif, rand);
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                            {
                                if (rand.Next(0, Strength) == 0)
                                {
                                    DrawNoiseColor(i, j, fillColor, r, g, b, dif, rand);
                                }
                            }
                        }
                    }

                pictureBox.Image = bitmap;
            }
            catch (Exception ee) { }
        }

        private void DrawNoiseColor(int i, int j, Color fillColor, int r, int g, int b, int dif, Random rand)
        {
            r = fillColor.R;
            g = fillColor.G;
            b = fillColor.B;

            dif = rand.Next(-32, 32);
            r = r + dif;
            g = g + dif;
            b = b + dif;

            if (r > 255) r = 255;
            if (r < 0) r = 0;
            if (g > 255) g = 255;
            if (g < 0) g = 0;
            if (b > 255) b = 255;
            if (b < 0) b = 0;

            bitmap.SetPixel(i, j, Color.FromArgb(r, g, b));
        }

        public void FillNoise(int x, int y, int brushSize)
        {
            int xSize = x - brushSize / 2;
            int ySize = y - brushSize / 2;
            Random rand = new Random();
            int r = 0, g = 0, b = 0, dif = 0;
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
                                    if (rand.Next(0, Strength) == 0)
                                    {
                                        DrawNoise(i, j, bitmap.GetPixel(i, j), r, g, b, dif, rand);
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                            {
                                if (rand.Next(0, Strength) == 0)
                                {
                                    DrawNoise(i, j, bitmap.GetPixel(i, j), r, g, b, dif, rand);
                                }
                            }
                        }
                    }

                pictureBox.Image = bitmap;
            }
            catch (Exception ee) { }
        }

        private void DrawNoise(int i, int j, Color curColor, int r, int g, int b, int dif, Random rand)
        {
            r = curColor.R;
            g = curColor.G;
            b = curColor.B;

            dif = rand.Next(-16, 16);
            r = r + dif;
            g = g + dif;
            b = b + dif;

            if (r > 255) r = 255;
            if (r < 0) r = 0;
            if (g > 255) g = 255;
            if (g < 0) g = 0;
            if (b > 255) b = 255;
            if (b < 0) b = 0;

            bitmap.SetPixel(i, j, Color.FromArgb(r, g, b));
        }

        public void FillBlur(int x, int y, int brushSize)
        {
            int xSize = x - brushSize / 2;
            int ySize = y - brushSize / 2;
            Random rand = new Random();
            int r, g, b, dif;
            var selectionBounds = graphics.VisibleClipBounds;

            double center = 0;
            double neib = 0;
            double stdev = 1;

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
                                    center = SmoothGaussCalc(i, j, stdev);
                                    if (center <= 0.01) center = 0.01;
                                    neib = center / 2;

                                    var curPixel = bitmap.GetPixel(i, j);
                                    var p1 = bitmap.GetPixel(i - 1, j - 1);
                                    var p2 = bitmap.GetPixel(i, j + 1);
                                    var p3 = bitmap.GetPixel(i, j - 1);
                                    var p4 = bitmap.GetPixel(i + 1, j - 1);
                                    var p5 = bitmap.GetPixel(i - 1, j);
                                    var p6 = bitmap.GetPixel(i + 1, j);
                                    var p7 = bitmap.GetPixel(i - 1, j + 1);
                                    var p8 = bitmap.GetPixel(i + 1, j + 1);

                                    int avgR = Convert.ToInt32((curPixel.R * center +
                                        p1.R * neib +
                                        p2.R * neib +
                                        p3.R * neib +
                                        p4.R * neib +
                                        p5.R * neib +
                                        p6.R * neib +
                                        p7.R * neib +
                                        p8.R * neib) / (center + neib * 8));
                                    int avgG = Convert.ToInt32((curPixel.G * center +
                                        p1.G * neib +
                                        p2.G * neib +
                                        p3.G * neib +
                                        p4.G * neib +
                                        p5.G * neib +
                                        p6.G * neib +
                                        p7.G * neib +
                                        p8.G * neib) / (center + neib * 8));
                                    int avgB = Convert.ToInt32((curPixel.B * center +
                                        p1.B * neib +
                                        p2.B * neib +
                                        p3.B * neib +
                                        p4.B * neib +
                                        p5.B * neib +
                                        p6.B * neib +
                                        p7.B * neib +
                                        p8.B * neib) / (center + neib * 8));

                                    if (avgR > 255) avgR = 255;
                                    if (avgR < 0) avgR = 0;
                                    if (avgG > 255) avgG = 255;
                                    if (avgG < 0) avgG = 0;
                                    if (avgB > 255) avgB = 255;
                                    if (avgB < 0) avgB = 0;

                                    bitmap.SetPixel(i, j, Color.FromArgb(avgR, avgG, avgB));
                                    bitmap.SetPixel(i - 1, j - 1, Color.FromArgb(avgR, avgG, avgB));
                                    bitmap.SetPixel(i, j + 1, Color.FromArgb(avgR, avgG, avgB));
                                    bitmap.SetPixel(i, j - 1, Color.FromArgb(avgR, avgG, avgB));
                                    bitmap.SetPixel(i + 1, j - 1, Color.FromArgb(avgR, avgG, avgB));
                                    bitmap.SetPixel(i - 1, j, Color.FromArgb(avgR, avgG, avgB));
                                    bitmap.SetPixel(i + 1, j, Color.FromArgb(avgR, avgG, avgB));
                                    bitmap.SetPixel(i - 1, j + 1, Color.FromArgb(avgR, avgG, avgB));
                                    bitmap.SetPixel(i + 1, j + 1, Color.FromArgb(avgR, avgG, avgB));
                                }
                            }
                        }

                        else
                        {
                            if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                            {
                                center = SmoothGaussCalc(i, j, stdev);
                                if (center <= 0.01) center = 0.01;
                                neib = center / 2;

                                var curPixel = bitmap.GetPixel(i, j);
                                var p1 = bitmap.GetPixel(i - 1, j - 1);
                                var p2 = bitmap.GetPixel(i, j + 1);
                                var p3 = bitmap.GetPixel(i, j - 1);
                                var p4 = bitmap.GetPixel(i + 1, j - 1);
                                var p5 = bitmap.GetPixel(i - 1, j);
                                var p6 = bitmap.GetPixel(i + 1, j);
                                var p7 = bitmap.GetPixel(i - 1, j + 1);
                                var p8 = bitmap.GetPixel(i + 1, j + 1);

                                int avgR = Convert.ToInt32((curPixel.R * center +
                                    p1.R * neib +
                                    p2.R * neib +
                                    p3.R * neib +
                                    p4.R * neib +
                                    p5.R * neib +
                                    p6.R * neib +
                                    p7.R * neib +
                                    p8.R * neib) / (center + neib * 8));
                                int avgG = Convert.ToInt32((curPixel.G * center +
                                    p1.G * neib +
                                    p2.G * neib +
                                    p3.G * neib +
                                    p4.G * neib +
                                    p5.G * neib +
                                    p6.G * neib +
                                    p7.G * neib +
                                    p8.G * neib) / (center + neib * 8));
                                int avgB = Convert.ToInt32((curPixel.B * center +
                                    p1.B * neib +
                                    p2.B * neib +
                                    p3.B * neib +
                                    p4.B * neib +
                                    p5.B * neib +
                                    p6.B * neib +
                                    p7.B * neib +
                                    p8.B * neib) / (center + neib * 8));

                                if (avgR > 255) avgR = 255;
                                if (avgR < 0) avgR = 0;
                                if (avgG > 255) avgG = 255;
                                if (avgG < 0) avgG = 0;
                                if (avgB > 255) avgB = 255;
                                if (avgB < 0) avgB = 0;

                                bitmap.SetPixel(i, j, Color.FromArgb(avgR, avgG, avgB));
                                bitmap.SetPixel(i - 1, j - 1, Color.FromArgb(avgR, avgG, avgB));
                                bitmap.SetPixel(i, j + 1, Color.FromArgb(avgR, avgG, avgB));
                                bitmap.SetPixel(i, j - 1, Color.FromArgb(avgR, avgG, avgB));
                                bitmap.SetPixel(i + 1, j - 1, Color.FromArgb(avgR, avgG, avgB));
                                bitmap.SetPixel(i - 1, j, Color.FromArgb(avgR, avgG, avgB));
                                bitmap.SetPixel(i + 1, j, Color.FromArgb(avgR, avgG, avgB));
                                bitmap.SetPixel(i - 1, j + 1, Color.FromArgb(avgR, avgG, avgB));
                                bitmap.SetPixel(i + 1, j + 1, Color.FromArgb(avgR, avgG, avgB));
                            }
                        }
                    }

                pictureBox.Image = bitmap;
            }
            catch (Exception ee) { }
        }

        public void FillGray(int x, int y, int brushSize)
        {
            int xSize = x - brushSize / 2;
            int ySize = y - brushSize / 2;
            int r = 0, g = 0, b = 0, avg = 0, difR = 0, difG = 0, difB = 0;
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
                                    DrawGray(i, j, bitmap.GetPixel(i, j), r, g, b, avg, difR, difG, difB);
                                }
                            }
                        }

                        else
                        {
                            if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                            {
                                DrawGray(i, j, bitmap.GetPixel(i, j), r, g, b, avg, difR, difG, difB);
                            }
                        }
                    }

                pictureBox.Image = bitmap;
            }
            catch (Exception ee) { }
        }

        private void DrawGray(int i, int j, Color curColor, int r, int g, int b, int avg, int difR, int difG, int difB)
        {
            r = curColor.R;
            g = curColor.G;
            b = curColor.B;

            avg = Convert.ToInt32((r + g + b) / 3.0);

            difR = r - avg;
            difG = g - avg;
            difB = b - avg;

            r = Convert.ToInt32(r - difR * 0.15);
            g = Convert.ToInt32(g - difG * 0.15);
            b = Convert.ToInt32(b - difB * 0.15);

            if (r > 255) r = 255;
            if (r < 0) r = 0;
            if (g > 255) g = 255;
            if (g < 0) g = 0;
            if (b > 255) b = 255;
            if (b < 0) b = 0;

            bitmap.SetPixel(i, j, Color.FromArgb(r, g, b));
        }

        private double SmoothGaussCalc(int x, int y, double stdev)
        {
            double G = 0;
            G = 1.0 / (2.0 * Math.PI * Math.Pow(stdev, 2)) * Math.Exp(-1.0 * (x * x + y * y) / (2.0 * Math.Pow(stdev, 2)));
            return G;
        }
    }
}
