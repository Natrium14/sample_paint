using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintGradient: PaintLine
    {
        public PaintGradient(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.graphics = graphics;
            this.startPoint = new Point(0, 0);
            this.endPoint = new Point(0, 0);
        }

        public void Fill(Color color1, Color color2) // пока что круговой от одной точки
        {
            int x1 = startPoint.X;
            int y1 = startPoint.Y;
            int x2 = endPoint.X;
            int y2 = endPoint.Y;
            Color colorN;

            double rast = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double rastN, percent, percentPrev = 1;
            var selectionBounds = graphics.VisibleClipBounds;

            for (int i = 1; i < pictureBox.Width - 3; i++)
                for (int j = 1; j < pictureBox.Height - 3; j++)
                {
                    if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                    {
                        rastN = Math.Sqrt(Math.Pow(i - x1, 2) + Math.Pow(j - y1, 2));
                        percent = 1 - rastN / rast;

                        int r = Convert.ToInt32(percent * (color2.R - color1.R) + color1.R);
                        int g = Convert.ToInt32(percent * (color2.G - color1.G) + color1.G);
                        int b = Convert.ToInt32(percent * (color2.B - color1.B) + color1.B);
                        if (r >= 255) r = 255;
                        if (r <= 0) r = 0;
                        if (g >= 255) g = 255;
                        if (g <= 0) g = 0;
                        if (b >= 255) b = 255;
                        if (b <= 0) b = 0;

                        colorN = Color.FromArgb(r, g, b);

                        bitmap.SetPixel(i, j, colorN);
                    }
                }

            pictureBox.Image = bitmap;
        }

        public void FillXOR(Color color1, Color color2) // пока что круговой от одной точки
        {
            int x1 = startPoint.X;
            int y1 = startPoint.Y;
            int x2 = endPoint.X;
            int y2 = endPoint.Y;
            Color colorN;

            double rast = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double rastN, percent, percentPrev = 1;
            var selectionBounds = graphics.VisibleClipBounds;

            for (int i = 1; i < pictureBox.Width - 3; i++)
                for (int j = 1; j < pictureBox.Height - 3; j++)
                {
                    if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                    {
                        rastN = Math.Sqrt(Math.Pow(i - x1, 2) + Math.Pow(j - y1, 2));
                        percent = 1 - rastN / rast;

                        var c = bitmap.GetPixel(i, j);

                        int r = Convert.ToInt32(percent * (color2.R - color1.R) + color1.R) ^ c.R;
                        int g = Convert.ToInt32(percent * (color2.G - color1.G) + color1.G) ^ c.G;
                        int b = Convert.ToInt32(percent * (color2.B - color1.B) + color1.B) ^ c.B;
                        if (r >= 255) r = 255;
                        if (r <= 0) r = 0;
                        if (g >= 255) g = 255;
                        if (g <= 0) g = 0;
                        if (b >= 255) b = 255;
                        if (b <= 0) b = 0;

                        colorN = Color.FromArgb(r, g, b);

                        bitmap.SetPixel(i, j, colorN);
                    }
                }

            pictureBox.Image = bitmap;
        }
    }
}
