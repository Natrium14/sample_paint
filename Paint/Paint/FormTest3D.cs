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

            myFigure = new MyFigure();
            myFigureCube = new MyFigureCube(p0);
            myFigurePrizm = new MyFigurePrizm(p0);

            myFigure = myFigureCube;

            myPoints = new List<MyPoint>();
            myPoints = myFigure.myPoints;

            angle0 = 0;
            angleProtiv = angle0;
            angleVniz = angle0;
            anglePo = angle0;
            angleVverh = angle0;
            angleChange = 0.3;

            brush1 = new SolidBrush(Color.FromArgb(70, 120, 150, 120));
            brush2 = new SolidBrush(Color.FromArgb(100, 120, 120, 150));
        }

        private Graphics g;
        private Bitmap bitmap;
        Point p0;
        List<MyPoint> myPoints;
        int angle0;
        double angleChange, angleProtiv, angleVniz, anglePo, angleVverh;
        SolidBrush brush1, brush2;
        private System.Timers.Timer timer;

        IMyFigure myFigure;
        IMyFigure myFigureCube;
        IMyFigure myFigurePrizm;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(Color.White);
            g.DrawEllipse(Pens.Black, p0.X - 1, p0.Y - 1, 3, 3);

            foreach (var p in myPoints)
            {
                g.DrawEllipse(Pens.Red, p.point.X - 1, p.point.Y - 1, 3, 3);

                foreach (var l in p.links)
                {
                    g.DrawLine(Pens.Black, 
                        myPoints.FirstOrDefault(x=>x.number == l.number1).point, 
                        myPoints.FirstOrDefault(x=>x.number == l.number2).point);
                }
            }

            myFigure.FillPoly(g, brush1);            

            pictureBox1.Image = bitmap;
        }

        class MyPoint
        {
            public int number;
            public Point point;
            public Point point0 { get; set; }
            public int lengthY;
            public int lengthX;
            public int angleX { get; set; }
            public int angleY { get; set; }
            public List<MyLink> links;

            public MyPoint(Point point, int number, Point p0, int angleX, int angleY, List<MyLink> links)
            {
                this.number = number;
                this.point = point;
                point0 = p0;
                this.angleX = angleX;
                this.angleY = angleY;
                this.links = links;
                lengthY = Convert.ToInt32(Math.Sqrt(Math.Pow(point.Y - point0.Y, 2)));
                lengthX = Convert.ToInt32(Math.Sqrt(Math.Pow(point.X - point0.X, 2)));
            }
        }

        class MyLink
        {
            public int number1;
            public int number2;
        }

        interface IMyFigure
        {
            List<MyPoint> myPoints { get; set; }
            void FillPoly(Graphics g, SolidBrush brush);
        }

        class MyFigure : IMyFigure
        {
            public List<MyPoint> myPoints
            { get; set; }
            public virtual void FillPoly(Graphics g, SolidBrush brush)
            {
                throw new NotImplementedException();
            }
        }

        class MyFigureCube : MyFigure
        {
            public MyFigureCube(Point p0)
            {
                Point p1 = new Point(p0.X - 70, p0.Y - 70);
                Point p2 = new Point(p0.X + 70, p0.Y - 70);
                Point p3 = new Point(p0.X - 70, p0.Y - 70);
                Point p4 = new Point(p0.X + 70, p0.Y - 70);

                Point p5 = new Point(p0.X - 70, p0.Y + 70);
                Point p6 = new Point(p0.X + 70, p0.Y + 70);
                Point p7 = new Point(p0.X - 70, p0.Y + 70);
                Point p8 = new Point(p0.X + 70, p0.Y + 70);
                myPoints = new List<MyPoint>();

                myPoints.Add(new MyPoint(p1, 1, p0, 305, 305, new List<MyLink>()));
                myPoints.Add(new MyPoint(p2, 2, p0, 225, 305, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 2
                    }
                }));
                myPoints.Add(new MyPoint(p3, 3, p0, 45, 225, new List<MyLink>()
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 3
                    }
                }));
                myPoints.Add(new MyPoint(p4, 4, p0, 135, 225, new List<MyLink>()
                { 
                    new MyLink(){
                        number1 = 2,
                        number2 = 4
                    },
                    new MyLink(){
                        number1 = 3,
                        number2 = 4
                    },
                }));
                myPoints.Add(new MyPoint(p5, 5, p0, 305, 45, new List<MyLink>()
                {
                    new MyLink(){
                        number1 = 1,
                        number2 = 5
                    }
                }));
                myPoints.Add(new MyPoint(p6, 6, p0, 225, 45, new List<MyLink>()
                { 
                    new MyLink(){
                        number1 = 5,
                        number2 = 6
                    },
                    new MyLink()
                    {
                        number1 = 2,
                        number2 = 6
                    }
                }));
                myPoints.Add(new MyPoint(p7, 7, p0, 45, 135, new List<MyLink>()
                { 
                    new MyLink(){
                        number1 = 5,
                        number2 = 7
                    },
                    new MyLink(){
                        number1 = 3,
                        number2 = 7
                    }
                }));
                myPoints.Add(new MyPoint(p8, 8, p0, 135, 135, new List<MyLink>()
                { 
                    new MyLink(){
                        number1 = 6,
                        number2 = 8
                    },
                    new MyLink(){
                        number1 = 7,
                        number2 = 8
                    },
                    new MyLink(){
                        number1 = 4,
                        number2 = 8
                    }
                }));
            }
            public override void FillPoly(Graphics g, SolidBrush brush)
            {
                g.FillPolygon(brush, new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 1).point,
                    myPoints.FirstOrDefault(x=>x.number == 2).point,
                    myPoints.FirstOrDefault(x=>x.number == 4).point,
                    myPoints.FirstOrDefault(x=>x.number == 3).point
                });
                g.FillPolygon(brush, new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 5).point,
                    myPoints.FirstOrDefault(x=>x.number == 6).point,
                    myPoints.FirstOrDefault(x=>x.number == 8).point,
                    myPoints.FirstOrDefault(x=>x.number == 7).point
                });
            }
        }

        class MyFigurePrizm : MyFigure
        {
            public MyFigurePrizm(Point p0)
            {
                Point p1 = new Point(p0.X, p0.Y - 50);
                Point p2 = new Point(p0.X - 50, p0.Y + 30);
                Point p3 = new Point(p0.X + 50, p0.Y + 30);
                Point p4 = new Point(p0.X - 50, p0.Y + 30);
                Point p5 = new Point(p0.X + 50, p0.Y + 30);

                myPoints = new List<MyPoint>();
                myPoints.Add(new MyPoint(p1, 1, p0, 0, 270, new List<MyLink>()));
                myPoints.Add(new MyPoint(p2, 2, p0, 305, 45, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 2
                    }
                }));
                myPoints.Add(new MyPoint(p3, 3, p0, 225, 45, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 3
                    },
                    new MyLink(){
                        number1 = 2,
                        number2 = 3
                    }
                }));
                myPoints.Add(new MyPoint(p4, 4, p0, 45, 135, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 4
                    },
                    new MyLink(){
                        number1 = 2,
                        number2 = 4
                    }
                }));
                myPoints.Add(new MyPoint(p5, 5, p0, 135, 135, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 5
                    },
                    new MyLink(){
                        number1 = 3,
                        number2 = 5
                    },
                    new MyLink(){
                        number1 = 4,
                        number2 = 5
                    }
                }));
            }
            public override void FillPoly(Graphics g, SolidBrush brush)
            {
                g.FillPolygon(brush, new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 2).point,
                    myPoints.FirstOrDefault(x=>x.number == 3).point,
                    myPoints.FirstOrDefault(x=>x.number == 5).point,
                    myPoints.FirstOrDefault(x=>x.number == 4).point
                });
            }
        }

        private void button1_Click(object sender, EventArgs e) // против часовой
        {
            //PovernutProtiv();
            Loopy();
        }

        void Loopy()
        {
            timer = new System.Timers.Timer(10);
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            PovernutProtiv();
            PovernutVniz();
        }

        private void button2_Click(object sender, EventArgs e) // по часовой
        {
            PovernutPo();
        }

        private void PovernutProtiv()
        {
            angleProtiv += angleChange;

            for (int i = 0; i < myPoints.Count; i++)
            {
                var a1 = Math.PI * (angleProtiv + myPoints[i].angleX) / 180.0;
                var b1 = Math.Cos(a1);
                int k1 = Convert.ToInt32(myPoints[i].lengthX * b1);
                myPoints[i].point.X = myPoints[i].point0.X + k1;
            }

            pictureBox1.Invalidate();
        }

        private void PovernutPo()
        {
            anglePo += angleChange;

            for (int i = 0; i < myPoints.Count; i++)
            {
                var a1 = Math.PI * (anglePo + myPoints[i].angleX) / 180.0;
                var b1 = Math.Cos(a1);
                int k1 = Convert.ToInt32(myPoints[i].lengthX * b1);
                myPoints[i].point.X = myPoints[i].point0.X - k1;
            }

            pictureBox1.Invalidate();
        }

        private void PovernutVniz()
        {
            angleVniz += angleChange;

            for (int i = 0; i < myPoints.Count; i++)
            {
                var a1 = Math.PI * (angleVniz + myPoints[i].angleY) / 180.0;
                var b1 = Math.Cos(a1);
                int k1 = Convert.ToInt32(myPoints[i].lengthY * b1);
                myPoints[i].point.Y = myPoints[i].point0.Y + k1;
            }

            pictureBox1.Invalidate();
        }

        private void PovernutVverh()
        {
            angleVverh += angleChange;

            for (int i = 0; i < myPoints.Count; i++)
            {
                var a1 = Math.PI * (angleVverh + myPoints[i].angleY) / 180.0;
                var b1 = Math.Cos(a1);
                int k1 = Convert.ToInt32(myPoints[i].lengthY * b1);
                myPoints[i].point.Y = myPoints[i].point0.Y - k1;
            }

            pictureBox1.Invalidate();
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
            PovernutVverh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PovernutVniz();
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
                myFigure = myFigurePrizm;
                myPoints = myFigure.myPoints;
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
                myFigure = myFigureCube;
                myPoints = myFigure.myPoints;
            }
        }

        
    }
}
