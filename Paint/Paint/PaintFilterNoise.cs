using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintFilterNoise: PaintFilter
    {
        public PaintFilterNoise(PictureBox pictureBox, Bitmap bitmap, ProgressBar progressBar)
            : base(pictureBox, bitmap, progressBar)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.progressBar = progressBar;
        }


        public void AddNoise(int strength)
        {
            progressBar.Value = 0;

            Random rand = new Random();
            for (int i = 0; i < pictureBox.Width - 2; i++)
            {
                SetValueProgressBar(i);

                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    if (rand.Next(0, strength) == 0)
                    {
                        var curPixel = bitmap.GetPixel(i, j);
                        int r = curPixel.R;
                        int g = curPixel.G;
                        int b = curPixel.B;

                        int dif = rand.Next(-32, 32);
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
                }
            }

            pictureBox.Image = bitmap;
            flag1 = true;
            flag2 = true;
            flag3 = true;
            flag4 = true;
        }
    }
}
