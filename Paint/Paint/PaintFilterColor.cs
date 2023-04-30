using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintFilterColor : PaintFilter
    {
        public PaintFilterColor(PictureBox pictureBox, Bitmap bitmap, ProgressBar progressBar)
            : base(pictureBox, bitmap, progressBar)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.progressBar = progressBar;
        }


        public void ToBlackWhite()
        {
            progressBar.Value = 0;

            for (int i = 0; i < pictureBox.Width - 2; i++)
            {
                SetValueProgressBar(i);
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R;
                    int g = curPixel.G;
                    int b = curPixel.B;
                    int avg = Convert.ToInt32((r + g + b) / 3.0);
                    bitmap.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                }
            }

            pictureBox.Image = bitmap;
            flag1 = true;
            flag2 = true;
            flag3 = true;
            flag4 = true;
        }

        public void Invert()
        {
            progressBar.Value = 0;

            for (int i = 0; i < pictureBox.Width - 2; i++)
            {
                SetValueProgressBar(i);
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R ^ 255;
                    int g = curPixel.G ^ 255;
                    int b = curPixel.B ^ 255;

                    if (r > 255) r = 255;
                    if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;

                    bitmap.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            pictureBox.Image = bitmap;
            flag1 = true;
            flag2 = true;
            flag3 = true;
            flag4 = true;
        }

        public void ChangeBrightness(double value)
        {
            progressBar.Value = 0;

            for (int i = 0; i < pictureBox.Width - 2; i++)
            {
                SetValueProgressBar(i);
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = Convert.ToInt32(curPixel.R + 255 * value); //
                    int g = Convert.ToInt32(curPixel.G + 255 * value); //
                    int b = Convert.ToInt32(curPixel.B + 255 * value); //

                    if (r > 255) r = 255;
                    if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;

                    bitmap.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            pictureBox.Image = bitmap;
            flag1 = true;
            flag2 = true;
            flag3 = true;
            flag4 = true;
        }

        public void AddColorChanel(int value, string chanel)
        {
            for (int i = 0; i < pictureBox.Width - 2; i++)
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R;
                    int g = curPixel.G;
                    int b = curPixel.B;

                    switch (chanel)
                    {
                        case "red":
                            r = r + value;
                            if (r > 255) r = 255;
                            if (r < 0) r = 0;
                            break;
                        case "green":
                            g = g + value;
                            if (g > 255) g = 255;
                            if (g < 0) g = 0;
                            break;
                        case "blue":
                            b = b + value;
                            if (b > 255) b = 255;
                            if (b < 0) b = 0;
                            break;
                    }


                    bitmap.SetPixel(i, j, Color.FromArgb(r, g, b));
                }

            pictureBox.Image = bitmap;
        }

        public void ToRedMax()
        {
            for (int i = 0; i < pictureBox.Width - 2; i++)
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R;
                    int g = curPixel.G;
                    int b = curPixel.B;
                    if (r >= 200 && g <= 200 && b <= 200)
                    {
                        r = 255;
                        bitmap.SetPixel(i, j, Color.FromArgb(r, 0, 0));
                    }
                    else
                    {
                        int avg = Convert.ToInt32((r + g + b) / 3.0);
                        bitmap.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                    }
                }

            pictureBox.Image = bitmap;
        }

        public void ToGreenMax()
        {
            for (int i = 0; i < pictureBox.Width - 2; i++)
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R;
                    int g = curPixel.G;
                    int b = curPixel.B;
                    if (g >= 200 && r <= 200 && b <= 200)
                    {
                        g = 255;
                        bitmap.SetPixel(i, j, Color.FromArgb(0, g, 0));
                    }
                    else
                    {
                        int avg = Convert.ToInt32((r + g + b) / 3.0);
                        bitmap.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                    }
                }

            pictureBox.Image = bitmap;
        }

        public void ToBlueMax()
        {
            for (int i = 0; i < pictureBox.Width - 2; i++)
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R;
                    int g = curPixel.G;
                    int b = curPixel.B;
                    if (b >= 200 && r <= 200 && g <= 200)
                    {
                        b = 255;
                        bitmap.SetPixel(i, j, Color.FromArgb(0, 0, b));
                    }
                    else
                    {
                        int avg = Convert.ToInt32((r + g + b) / 3.0);
                        bitmap.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                    }
                }

            pictureBox.Image = bitmap;
        }

        public void ToRedGray()
        {
            for (int i = 0; i < pictureBox.Width - 2; i++)
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R;
                    int g = curPixel.G;
                    int b = curPixel.B;
                    int avg = Convert.ToInt32((r + g + b) / 3.0);
                    if (r >= 0)
                    {
                        r = 255;
                    }
                    else
                    {
                        r = 0;
                    }
                    bitmap.SetPixel(i, j, Color.FromArgb(r, avg, avg));
                }

            pictureBox.Image = bitmap;
        }

        public void ToGreenGray()
        {
            for (int i = 0; i < pictureBox.Width - 2; i++)
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R;
                    int g = curPixel.G;
                    int b = curPixel.B;
                    int avg = Convert.ToInt32((r + g + b) / 3.0);
                    if (g >= 0)
                    {
                        g = 255;
                    }
                    else
                    {
                        g = 0;
                    }
                    bitmap.SetPixel(i, j, Color.FromArgb(avg, g, avg));
                }

            pictureBox.Image = bitmap;
        }

        public void ToBlueGray()
        {
            for (int i = 0; i < pictureBox.Width - 2; i++)
                for (int j = 0; j < pictureBox.Height - 2; j++)
                {
                    var curPixel = bitmap.GetPixel(i, j);
                    int r = curPixel.R;
                    int g = curPixel.G;
                    int b = curPixel.B;
                    int avg = Convert.ToInt32((r + g + b) / 3.0);
                    if (b >= 0)
                    {
                        b = 255;
                    }
                    else
                    {
                        b = 0;
                    }
                    bitmap.SetPixel(i, j, Color.FromArgb(avg, avg, b));
                }

            pictureBox.Image = bitmap;
        }
    }
}
