using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintWave: PaintLine
    {
        public PaintWave(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {

        }

        public void DrawWave(double B, double V) //коэф затухания, частота
        {
            int A = 127, AA, AB, SUM; // начальная амплитуда
            //double B = 4.0; // коэф затухания
            //double V = 1.6; // частота
            double t, b, c;

            int x1 = startPoint.X;
            int y1 = startPoint.Y;
            int x2 = endPoint.X;
            int y2 = endPoint.Y;
            Color colorN;
            double rast = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double percent, percentPrev1 = 1, percentPrev2 = 1;

            for (int i = 1; i < pictureBox.Width - 3; i++)
                for (int j = 1; j < pictureBox.Height - 3; j++)
                {
                    t = Math.Sqrt(Math.Pow(i - x1, 2) + Math.Pow(j - y1, 2));
                    b = Math.Exp(-1 * B / 1000 * t);
                    c = Math.Sin(V/1000 * t * 180 / Math.PI);
                    percent = 1 - t / rast;
                    if (percent <= percentPrev1) percent = percentPrev1;
                    AA = Convert.ToInt32(A * b * c * percent) + 127;
                    if (AA > 255) AA = 255;
                    if (AA < 0) AA = 0;
                    percentPrev1 = percent;

                    t = Math.Sqrt(Math.Pow(i - x2, 2) + Math.Pow(j - y2, 2));
                    b = Math.Exp(-1 * B / 1000 * t);
                    c = Math.Sin(V / 1000 * t * 180 / Math.PI);
                    percent = 1 - t / rast;
                    if (percent <= percentPrev2) percent = percentPrev2;
                    AB = Convert.ToInt32(A * b * c * percent) + 127;
                    if (AB > 255) AB = 255;
                    if (AB < 0) AB = 0;
                    percentPrev2 = percent;

                    SUM = Math.Abs(AA - AB);
                    if (SUM > 255) SUM = 255;
                    if (SUM < 0) SUM = 0;

                    colorN = Color.FromArgb(Convert.ToInt32(SUM * 1), Convert.ToInt32(SUM * 1), Convert.ToInt32(SUM * 1));

                    bitmap.SetPixel(i, j, colorN);
                }

            pictureBox.Image = bitmap;
        }

        public void PaintDrawWave(double B, double V) //коэф затухания, частота
        {
            int AR,AB,AG;
            double t, b, c;

            int x1 = startPoint.X;
            int y1 = startPoint.Y;
            int x2 = endPoint.X;
            int y2 = endPoint.Y;
            Color colorN, colorCur;
            double rast = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double percent, percentPrev1 = 1;

            for (int i = 1; i < pictureBox.Width - 3; i++)
                for (int j = 1; j < pictureBox.Height - 3; j++)
                {
                    colorCur = bitmap.GetPixel(i, j);
                    t = Math.Sqrt(Math.Pow(i - x1, 2) + Math.Pow(j - y1, 2));
                    b = Math.Exp(-1 * B / 1000 * t);
                    c = Math.Sin(V / 1000 * t * 180 / Math.PI);
                    percent = 1 - t / rast;
                    if (percent <= percentPrev1) percent = percentPrev1;
                    AR = Convert.ToInt32(colorCur.R * b * c * percent) + colorCur.R;
                    AG = Convert.ToInt32(colorCur.G * b * c * percent) + colorCur.G;
                    AB = Convert.ToInt32(colorCur.B * b * c * percent) + colorCur.B;
                    if (AR > 255) AR = 255;
                    if (AR < 0) AR = Math.Abs(AR);
                    if (AG > 255) AG = 255;
                    if (AG < 0) AG = Math.Abs(AG);
                    if (AB > 255) AB = 255;
                    if (AB < 0) AB = Math.Abs(AB);
                    percentPrev1 = percent;

                    colorN = Color.FromArgb(Convert.ToInt32(AR * 1), Convert.ToInt32(AG * 1), Convert.ToInt32(AB * 1));

                    bitmap.SetPixel(i, j, colorN);
                }

            pictureBox.Image = bitmap;
        }
    }
}
