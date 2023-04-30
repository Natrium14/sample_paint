using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintFilterSmooth: PaintFilter
    {
        public PaintFilterSmooth(PictureBox pictureBox, Bitmap bitmap, ProgressBar progressBar)
            : base(pictureBox, bitmap, progressBar)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.progressBar = progressBar;
        }

        public void Smooth() //  vertical
        {
            for (int i = 0; i < pictureBox.Width - 2; i++)
                for (int j = 0; j < pictureBox.Height - 5; j = j + 1)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    var nextPixel = bitmap.GetPixel(i, j + 1);
                    var nextPixel2 = bitmap.GetPixel(i, j + 2);

                    int r1 = curPixel.R;
                    int g1 = curPixel.G;
                    int b1 = curPixel.B;

                    int r2 = nextPixel.R;
                    int g2 = nextPixel.G;
                    int b2 = nextPixel.B;

                    int r3 = nextPixel2.R;
                    int g3 = nextPixel2.G;
                    int b3 = nextPixel2.B;

                    int avgR = (r1 + r2 + r3) / 3;
                    int avgG = (g1 + g2 + g3) / 3;
                    int avgB = (b1 + b2 + b3) / 3;

                    bitmap.SetPixel(i, j, Color.FromArgb(avgR, avgG, avgB));
                    bitmap.SetPixel(i, j + 1, Color.FromArgb(avgR, avgG, avgB));
                    bitmap.SetPixel(i, j + 2, Color.FromArgb(avgR, avgG, avgB));
                }

            pictureBox.Image = bitmap;
        }

        public void Smooth2() // horizontal
        {
            for (int i = 0; i < pictureBox.Width - 5; i = i + 1)
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    var nextPixel = bitmap.GetPixel(i + 1, j);
                    var nextPixel2 = bitmap.GetPixel(i + 2, j);

                    int r1 = curPixel.R;
                    int g1 = curPixel.G;
                    int b1 = curPixel.B;

                    int r2 = nextPixel.R;
                    int g2 = nextPixel.G;
                    int b2 = nextPixel.B;

                    int r3 = nextPixel2.R;
                    int g3 = nextPixel2.G;
                    int b3 = nextPixel2.B;

                    int avgR = (r1 + r2 + r3) / 3;
                    int avgG = (g1 + g2 + g3) / 3;
                    int avgB = (b1 + b2 + b3) / 3;

                    bitmap.SetPixel(i, j, Color.FromArgb(avgR, avgG, avgB));
                    bitmap.SetPixel(i + 1, j, Color.FromArgb(avgR, avgG, avgB));
                    bitmap.SetPixel(i + 2, j, Color.FromArgb(avgR, avgG, avgB));
                }

            pictureBox.Image = bitmap;
        }

        public void Smooth3() // center 8pixels around
        {
            progressBar.Value = 0;

            for (int i = 1; i < pictureBox.Width - 3; i++)
            {
                SetValueProgressBar(i);

                for (int j = 1; j < pictureBox.Height - 3; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    var p1 = bitmap.GetPixel(i - 1, j - 1);
                    var p2 = bitmap.GetPixel(i, j + 1);
                    var p3 = bitmap.GetPixel(i, j - 1);
                    var p4 = bitmap.GetPixel(i + 1, j - 1);
                    var p5 = bitmap.GetPixel(i - 1, j);
                    var p6 = bitmap.GetPixel(i + 1, j);
                    var p7 = bitmap.GetPixel(i - 1, j + 1);
                    var p8 = bitmap.GetPixel(i + 1, j + 1);

                    int avgR = (curPixel.R +
                        p1.R +
                        p2.R +
                        p3.R +
                        p4.R +
                        p5.R +
                        p6.R +
                        p7.R +
                        p8.R) / 9;
                    int avgG = (curPixel.G +
                        p1.G +
                        p2.G +
                        p3.G +
                        p4.G +
                        p5.G +
                        p6.G +
                        p7.G +
                        p8.G) / 9;
                    int avgB = (curPixel.B +
                        p1.B +
                        p2.B +
                        p3.B +
                        p4.B +
                        p5.B +
                        p6.B +
                        p7.B +
                        p8.B) / 9;

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

            pictureBox.Image = bitmap;
            flag1 = true;
            flag2 = true;
            flag3 = true;
            flag4 = true;
        }

        public void SmoothGauss(double stdev)
        {
            double center = 0;
            double neib = 0;

            progressBar.Value = 0;

            for (int i = 1; i < pictureBox.Width - 4; i = i + 1)
            {
                SetValueProgressBar(i);
                for (int j = 1; j < pictureBox.Height - 4; j = j + 1)
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

            pictureBox.Image = bitmap;
            flag1 = true;
            flag2 = true;
            flag3 = true;
            flag4 = true;
        } // соседи 1 порядка

        public void SmoothGauss2(double stdev)
        {
            double center = 0;
            double neib = 0;
            double neib2 = 0;

            progressBar.Value = 0;

            for (int i = 3; i < pictureBox.Width - 6; i = i + 1)
            {
                SetValueProgressBar(i);
                for (int j = 3; j < pictureBox.Height - 6; j = j + 1)
                {
                    center = SmoothGaussCalc(i, j, stdev);
                    if (center <= 0.0001) center = 0.0001;
                    neib = center * 0.9;
                    neib2 = center * 0.8;

                    var curPixel = bitmap.GetPixel(i, j);
                    double[,] matrix = new double[,]{
                        {neib2, neib2, neib2, neib2, neib2},
                        {neib2,neib,neib,neib,neib2},
                        {neib2,neib,center,neib,neib2},
                        {neib2,neib,neib,neib,neib2},
                        {neib2, neib2, neib2, neib2, neib2}
                    };

                    double matrixSum = 0;
                    for (int k = 0; k < 5; k++)
                        for (int m = 0; m < 5; m++)
                            matrixSum += matrix[k, m];

                    int avgR, avgG, avgB;
                    double sumR = 0, sumG = 0, sumB = 0;

                    for (int k = -2; k < 3; k++)
                        for (int m = -2; m < 3; m++)
                        {
                            var pixel = bitmap.GetPixel(i + k, j + m);
                            sumR += pixel.R * matrix[k + 2, m + 2];
                            sumG += pixel.G * matrix[k + 2, m + 2];
                            sumB += pixel.B * matrix[k + 2, m + 2];
                        }

                    avgR = Convert.ToInt32(sumR / matrixSum);
                    avgG = Convert.ToInt32(sumG / matrixSum);
                    avgB = Convert.ToInt32(sumB / matrixSum);

                    if (avgR > 255) avgR = 255;
                    if (avgR < 0) avgR = 0;
                    if (avgG > 255) avgG = 255;
                    if (avgG < 0) avgG = 0;
                    if (avgB > 255) avgB = 255;
                    if (avgB < 0) avgB = 0;

                    for (int k = -2; k < 3; k++)
                    {
                        for (int m = -2; m < 3; m++)
                        {
                            bitmap.SetPixel(i + k, j + m, Color.FromArgb(avgR, avgG, avgB));
                        }
                    }
                }
            }

            pictureBox.Image = bitmap;
            flag1 = true;
            flag2 = true;
            flag3 = true;
            flag4 = true;
        } // cоседи 2 порядка

        public void SmoothSquare(int n) // 2 <= n <= 10
        {
            for (int i = 0; i <= pictureBox.Width - n; i = i + n)
                for (int j = 0; j <= pictureBox.Height - n; j = j + n)
                {
                    //Color avg;
                    int sumR = 0, sumG = 0, sumB = 0;
                    int avgR = 0, avgG = 0, avgB = 0;

                    for(int k=i; k<i+n; k++)
                        for (int m = j; m < j + n; m++)
                        {
                            var curPixel = bitmap.GetPixel(k, m);
                            sumR += curPixel.R;
                            sumG += curPixel.G;
                            sumB += curPixel.B;
                        }

                    avgR = sumR / (n * n);
                    avgG = sumG / (n * n);
                    avgB = sumB / (n * n);

                    for (int k = i; k < i + n; k++)
                        for (int m = j; m < j + n; m++)
                        {
                            bitmap.SetPixel(k, m, Color.FromArgb(avgR, avgG, avgB));
                        }
                }

            pictureBox.Image = bitmap;
        }

        private double SmoothGaussCalc(int x, int y, double stdev)
        {
            double G = 0;
            G = 1.0 / (2.0 * Math.PI * Math.Pow(stdev, 2)) * Math.Exp(-1.0 * (x * x + y * y) / (2.0 * Math.Pow(stdev, 2)));
            return G;
        }

    }
}
