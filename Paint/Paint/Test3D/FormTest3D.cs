using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Paint.Test3D;

namespace Paint
{
    public partial class FormTest3D : Form
    {
        public FormTest3D()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);

            p0 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            currentFigure = new MyFigure();
            myFigureCube = new MyFigureCube(p0);
            myFigurePrizm = new MyFigurePrizm(p0);
            myFigureRomb = new MyFigureRomb(p0);
            myFigureFiveRomb = new MyFigureFiveRomb(p0);

            currentFigure = myFigureCube;

            currentMyPoints = new List<MyPoint>();
            currentMyPoints = currentFigure.myPoints;
            
            brush1 = new SolidBrush(Color.FromArgb(70, 120, 150, 120));
            brush2 = new SolidBrush(Color.FromArgb(100, 120, 120, 150));
        }

        private Graphics g;
        private Bitmap bitmap;
        Point p0;
        List<MyPoint> currentMyPoints;
        SolidBrush brush1, brush2;
        private System.Timers.Timer timer;

        IMyFigure currentFigure;
        IMyFigure myFigureCube;
        IMyFigure myFigurePrizm;
        IMyFigure myFigureRomb;
        IMyFigure myFigureFiveRomb;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(Color.White);
            //g.DrawLine(new Pen(Color.Black, 0.3f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
            //g.DrawLine(new Pen(Color.Black, 0.3f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, pictureBox1.Width/2, 0, pictureBox1.Width / 2, pictureBox1.Height);
            //g.DrawEllipse(Pens.Black, p0.X - 1, p0.Y - 1, 2, 2);
            
            foreach (var p in currentMyPoints)
            {
                //g.DrawEllipse(Pens.Black, p.point.X, p.point.Y, 1, 1);

                foreach (var l in p.links)
                {
                    g.DrawLine(Pens.Black, 
                        currentMyPoints.FirstOrDefault(x => x.number == l.number1).point, 
                        currentMyPoints.FirstOrDefault(x => x.number == l.number2).point);
                }
            }

            currentFigure.FillPoly(g, brush1);            

            pictureBox1.Image = bitmap;
        }
        
        private void button1_Click(object sender, EventArgs e) // против часовой
        {
            //myFigure.PovernutProtiv(0.5);
            //pictureBox1.Invalidate();
            
            Loopy();
        }

        void Loopy()
        {
            timer = new System.Timers.Timer(5);
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            currentFigure.PovernutProtiv(0.3);
            currentFigure.PovernutVniz(0.3);
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e) // по часовой
        {
            //myFigure.PovernutPo(0.5);
            //pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //myFigure.PovernutVverh(0.5);
            //pictureBox1.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //myFigure.PovernutVniz(0.5);
            //pictureBox1.Invalidate();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            if (radioButton2.Checked)
            {
                currentFigure = myFigurePrizm;
                currentMyPoints = currentFigure.myPoints;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            if (radioButton1.Checked)
            {
                currentFigure = myFigureCube;
                currentMyPoints = currentFigure.myPoints;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            if (radioButton3.Checked)
            {
                currentFigure = myFigureRomb;
                currentMyPoints = currentFigure.myPoints;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            if (radioButton4.Checked)
            {
                currentFigure = myFigureFiveRomb;
                currentMyPoints = currentFigure.myPoints;
            }
        }

        
    }
}
