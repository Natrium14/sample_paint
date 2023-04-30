using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class FormPenPattern : Form
    {
        public FormPenPattern()
        {
            InitializeComponent();
        }

        public FormPenPattern(ref PaintPero paintPath)
        {
            InitializeComponent();
            this.paintPath = paintPath;
            patternPoints = new List<float>();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
        }

        private PaintPero paintPath;
        private List<float> patternPoints;
        private Graphics g;
        private Bitmap bitmap;

        private void FormPenPattern_Shown(object sender, EventArgs e)
        {
            //paintPath.myPen.CompoundArray = new float[] { 0.01f, 0.2f, 0.8f, 0.99f };
            //paintPath.PenWidth = 50.0f;
        }

        private void FormPenPattern_FormClosed(object sender, FormClosedEventArgs e)
        {
            paintPath.ca1 = 0.0f;
            paintPath.ca2 = 0.5f;
            paintPath.ca3 = 0.5f;
            paintPath.ca4 = 1.0f;
        }

        float p1Height = 0;
        float p2Height = 0;
        float p3Height = 0;
        float p4Height = 0;

        int y1Height = 0;
        int y2Height = 0;
        int y3Height = 0;
        int y4Height = 0;

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (patternPoints.Count == 0)
            {
                p1Height = Convert.ToSingle(Convert.ToDouble(e.Y) / Convert.ToDouble(pictureBox1.Height));
                patternPoints.Add(p1Height);
                g.DrawLine(Pens.Black, 2, e.Y, pictureBox1.Width - 2, e.Y);
                y1Height = e.Y;
            }
            else
            {
                if (patternPoints.Count == 1)
                {
                    p2Height = Convert.ToSingle(Convert.ToDouble(e.Y) / Convert.ToDouble(pictureBox1.Height));
                    if (p2Height > p1Height)
                    {
                        patternPoints.Add(p2Height);
                        g.DrawLine(Pens.Black, 2, e.Y, pictureBox1.Width - 2, e.Y);
                        y2Height = e.Y;

                        for (int i = 1; i < pictureBox1.Width - 2; i++)
                            for (int j = y1Height; j < y2Height; j++)
                            {
                                bitmap.SetPixel(i, j, Color.Black);
                            }
                    }
                }
                else
                {
                    if (patternPoints.Count == 2)
                    {
                        p3Height = Convert.ToSingle(Convert.ToDouble(e.Y) / Convert.ToDouble(pictureBox1.Height));
                        if (p3Height > p2Height)
                        {
                            patternPoints.Add(p3Height);
                            g.DrawLine(Pens.Black, 2, e.Y, pictureBox1.Width - 2, e.Y);
                            y3Height = e.Y;
                        }
                    }
                    else
                    {
                        if (patternPoints.Count == 3)
                        {
                            p4Height = Convert.ToSingle(Convert.ToDouble(e.Y) / Convert.ToDouble(pictureBox1.Height));
                            if (p4Height > p3Height)
                            {
                                patternPoints.Add(p4Height);
                                g.DrawLine(Pens.Black, 2, e.Y, pictureBox1.Width - 2, e.Y);
                                y4Height = e.Y;

                                for (int i = 1; i < pictureBox1.Width - 2; i++)
                                    for (int j = y3Height; j < y4Height; j++)
                                    {
                                        bitmap.SetPixel(i, j, Color.Black);
                                    }

                                paintPath.ca1 = p1Height;
                                paintPath.ca2 = p2Height;
                                paintPath.ca3 = p3Height;
                                paintPath.ca4 = p4Height;
                            }
                        }
                        else
                        {
                            if (patternPoints.Count == 4)
                            {
                                patternPoints.Clear();
                                for (int i = 1; i < pictureBox1.Width - 2; i++)
                                    for (int j = 1; j < pictureBox1.Height - 2; j++)
                                    {
                                        bitmap.SetPixel(i, j, Color.White);
                                    }
                            }
                        }
                    }
                }
            }

            pictureBox1.Image = bitmap;
        }
    }
}
