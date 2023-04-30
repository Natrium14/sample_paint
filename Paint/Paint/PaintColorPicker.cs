using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintColorPicker
    {
        private PictureBox pictureBox;
        private Bitmap bitmap;

        public PaintColorPicker(PictureBox pictureBox, Bitmap bitmap)
        {
            this.pictureBox = pictureBox;
            this.bitmap = bitmap;
        }

        /* Самый первый колорпикер 
        public void InitColorPanel()
        {
            Point colorR, colorG, colorB, colorO;
            int h;
            int colorWidth = 765;
            h = pictureBox.Width;
            colorR = new Point(0, pictureBox.Height);
            colorG = new Point(pictureBox.Width / 2, 0 - pictureBox.Height / 10);
            colorB = new Point(pictureBox.Width, pictureBox.Height);
            colorO = new Point(0, 0);

            double r1, r2, r3;
            for (int i = 0; i < pictureBox.Width; i++)
            {
                for (int j = 0; j < pictureBox.Height; j++)
                {
                    colorO.X = i;
                    colorO.Y = j;
                    r1 = Math.Sqrt(Math.Pow(colorR.X - colorO.X, 2) + Math.Pow(colorR.Y - colorO.Y, 2));
                    r2 = Math.Sqrt(Math.Pow(colorG.X - colorO.X, 2) + Math.Pow(colorG.Y - colorO.Y, 2));
                    r3 = Math.Sqrt(Math.Pow(colorB.X - colorO.X, 2) + Math.Pow(colorB.Y - colorO.Y, 2));
                    if (r1 <= h && r2 <= h && r3 <= h)
                    {
                        double r = (h - r1) / h * colorWidth >= 255 ? 255 : (h - r1) / h * colorWidth;
                        double g = (h - r2) / h * colorWidth >= 255 ? 255 : (h - r2) / h * colorWidth;
                        double b = (h - r3) / h * colorWidth >= 255 ? 255 : (h - r3) / h * colorWidth;
                        Color curColor = Color.FromArgb(255,(int)r, (int)g, (int)b);
                        Brush brushColor = new SolidBrush(curColor);
                        bitmap.SetPixel(i, j, curColor);
                    }
                }
            }

            pictureBox.Image = bitmap;
        }*/

        /* Улучшенный колорпикер colorWidth - ширина цветовой палитры
        public void InitColorPanelTest()
        {
            Point colorR, colorG, colorB, colorO;
            int h;
            int colorWidth = 64;
            h = pictureBox.Width;
            colorR = new Point(0, pictureBox.Height);
            colorG = new Point(pictureBox.Width / 2, 0 - pictureBox.Height / 10);
            colorB = new Point(pictureBox.Width, pictureBox.Height);
            colorO = new Point(0, 0);

            double r1, r2, r3;
            for (int i = 0; i < pictureBox.Width; i++)
            {
                for (int j = 0; j < pictureBox.Height; j++)
                {
                    colorO.X = i;
                    colorO.Y = j;
                    r1 = Math.Sqrt(Math.Pow(colorR.X - colorO.X, 2) + Math.Pow(colorR.Y - colorO.Y, 2));
                    r2 = Math.Sqrt(Math.Pow(colorG.X - colorO.X, 2) + Math.Pow(colorG.Y - colorO.Y, 2));
                    r3 = Math.Sqrt(Math.Pow(colorB.X - colorO.X, 2) + Math.Pow(colorB.Y - colorO.Y, 2));
                    double r = (h - r1) / h * colorWidth;
                    double g = (h - r2) / h * colorWidth;
                    double b = (h - r3) / h * colorWidth;

                    if (r <= 0)
                        r = 0;
                    if (r >= 255)
                        r = 255;
                    if (g <= 0)
                        g = 0;
                    if (g >= 255)
                        g = 255;
                    if (b <= 0)
                        b = 0;
                    if (b >= 255)
                        b = 255;

                    Color curColor = Color.FromArgb(255, (int)r, (int)g, (int)b);
                    Brush brushColor = new SolidBrush(curColor);
                    bitmap.SetPixel(i, j, curColor);
                }
            }

            pictureBox.Image = bitmap;
        }*/

        /* попытка сделать четырехсторонний колорпикер
       public void InitColorPanelTest2(int colorWidth)
       {
           Point colorR, colorG, colorB, colorA, colorO;
           int h;
           h = pictureBox.Width;
           colorR = new Point(0, pictureBox.Height);
           colorG = new Point(0, 0);
           colorB = new Point(pictureBox.Width, pictureBox.Height);
           colorA = new Point(pictureBox.Width, 0);
           colorO = new Point(0, 0);

           double r1, r2, r3, r4;
           for (int i = 0; i < pictureBox.Width; i++)
           {
               for (int j = 0; j < pictureBox.Height; j++)
               {
                   colorO.X = i;
                   colorO.Y = j;
                   r1 = Math.Sqrt(Math.Pow(colorR.X - colorO.X, 2) + Math.Pow(colorR.Y - colorO.Y, 2));
                   r2 = Math.Sqrt(Math.Pow(colorG.X - colorO.X, 2) + Math.Pow(colorG.Y - colorO.Y, 2));
                   r3 = Math.Sqrt(Math.Pow(colorB.X - colorO.X, 2) + Math.Pow(colorB.Y - colorO.Y, 2));
                   r4 = Math.Sqrt(Math.Pow(colorA.X - colorO.X, 2) + Math.Pow(colorA.Y - colorO.Y, 2)); // доработать
                   double r = (h - r1) / h * colorWidth;
                   double g = (h - r2) / h * colorWidth;
                   double b = (h - r3) / h * colorWidth;
                   double a = (h - r4) / h * colorWidth; // доработать

                   if (r <= 0)
                       r = 0;
                   if (r >= 255)
                       r = 255;
                   if (g <= 0)
                       g = 0;
                   if (g >= 255)
                       g = 255;
                   if (b <= 0)
                       b = 0;
                   if (b >= 255)
                       b = 255;
                   if (a <= 0) // условие доработать
                       a = 255;
                   if (a >= 255)
                       a = 255;

                   Color curColor = Color.FromArgb((int)a, (int)r, (int)g, (int)b);
                   Brush brushColor = new SolidBrush(curColor);
                   bitmap.SetPixel(i, j, curColor);
               }
           }

           pictureBox.Image = bitmap;
       } */

        // более ле менее колор пикер
        public void InitColorPanelTest(int colorWidth)
        {
            Point colorR, colorG, colorB, colorO;
            int h;
            h = pictureBox.Width;
            colorR = new Point(0, pictureBox.Height);
            colorG = new Point(pictureBox.Width / 2, 0 - pictureBox.Height / 10);
            colorB = new Point(pictureBox.Width, pictureBox.Height);
            colorO = new Point(0, 0);

            double r1, r2, r3;
            for (int i = 0; i < pictureBox.Width; i++)
            {
                for (int j = 0; j < pictureBox.Height; j++)
                {
                    colorO.X = i;
                    colorO.Y = j;
                    r1 = Math.Sqrt(Math.Pow(colorR.X - colorO.X, 2) + Math.Pow(colorR.Y - colorO.Y, 2));
                    r2 = Math.Sqrt(Math.Pow(colorG.X - colorO.X, 2) + Math.Pow(colorG.Y - colorO.Y, 2));
                    r3 = Math.Sqrt(Math.Pow(colorB.X - colorO.X, 2) + Math.Pow(colorB.Y - colorO.Y, 2));
                    double r = (h - r1) / h * colorWidth;
                    double g = (h - r2) / h * colorWidth;
                    double b = (h - r3) / h * colorWidth;

                    if (r <= 0) r = 0;
                    if (r >= 255) r = 255;
                    if (g <= 0) g = 0;
                    if (g >= 255) g = 255;
                    if (b <= 0) b = 0;
                    if (b >= 255) b = 255;

                    Color curColor = Color.FromArgb(255, (int)r, (int)g, (int)b);
                    bitmap.SetPixel(i, j, curColor);
                }
            }

            pictureBox.Image = bitmap;
        }

        // четырехсекционный колорпикер
        public void InitColorPanel4()
        {
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    Color curColor = Color.FromArgb(255, (int)(i * 255 / 128), (int)(j * 255 / 128), 255);
                    bitmap.SetPixel(i, j, curColor);
                }
            }

            for (int i = 255; i >= 128; i--)
            {
                for (int j = 0; j < 128; j++)
                {
                    Color curColor = Color.FromArgb(255, 255, (int)(j * 255 / 128), (int)((255 - i) * 255 / 128));
                    bitmap.SetPixel(i, j, curColor);
                }
            }

            for (int i = 0; i < 128; i++)
            {
                for (int j = 128; j < 255; j++)
                {
                    Color curColor = Color.FromArgb(255, (int)(i * 255 / 128), 255, (int)((255 - j) * 255 / 128));
                    bitmap.SetPixel(i, j, curColor);
                }
            }

            double aa = 2.0; // коэффициент
            double bb = 0.015594; // шаг изменение коэффциента
            for (int i = 128; i < 255; i++)
            {
                for (int j = 128; j < 255; j++)
                {
                    int a = (int)(i * (aa - bb));
                    Color curColor = Color.FromArgb(255, a, a, a);
                    bitmap.SetPixel(i, j, curColor);
                }

                bb += 0.015594;
            }

            pictureBox.Image = bitmap;
        }


    }
}
