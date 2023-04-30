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
    public partial class TestPolygon : Form
    {
        public TestPolygon()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);

            p0 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            MySquarePolygon aa = new MySquarePolygon(p0);
            foreach (var fe in aa.nodes)
            {
                g.FillRectangle(Brushes.Green, fe.point.X - 3, fe.point.Y - 3, 6, 6);
            }

            //g.FillEllipse(Brushes.Red, p0.X - 2, p0.Y - 2, 5, 5);
            /*
            MyPolygon p = new MyPolygon(p0, 50, 3, 0);

            foreach (var p1 in p.points)
            {
                //g.DrawLine(Pens.Black, p0, p1);
                g.FillEllipse(Brushes.Green, p1.X - 3, p1.Y - 3, 6, 6);
            }

           // p.FillPolygon(pictureBox1, bitmap, g, Color.Black);

            
            MyPolygon p2 = new MyPolygon(new Point(p.points[0].X - (p0.X - p.points[1].X), p.points[1].Y), 50, 3, 60);

            foreach (var p1 in p2.points)
            {
                g.DrawLine(Pens.Black, new Point(p.points[0].X - (p0.X - p.points[1].X), p.points[1].Y), p1);
                g.FillEllipse(Brushes.Green, p1.X - 3, p1.Y - 3, 6, 6);
            }

            MyPolygon p3 = new MyPolygon(new Point(p.points[0].X - (p0.X - p.points[1].X), p.points[2].Y), 50, 3, 60);

            foreach (var p1 in p3.points)
            {
                g.DrawLine(Pens.Black, new Point(p.points[0].X - (p0.X - p.points[1].X), p.points[2].Y), p1);
                g.FillEllipse(Brushes.Green, p1.X - 3, p1.Y - 3, 6, 6);
            }

            MyPolygon p4 = new MyPolygon(new Point(p.points[1].X - (p0.X - p.points[1].X), p.points[0].Y), 50, 3, 60);

            foreach (var p1 in p4.points)
            {
                g.DrawLine(Pens.Black, new Point(p.points[1].X - (p0.X - p.points[1].X), p.points[0].Y), p1);
                g.FillEllipse(Brushes.Green, p1.X - 3, p1.Y - 3, 6, 6);
            }
            

            MyPolygonGen a = new MyPolygonGen(pictureBox1, bitmap, g);
            a.G(p, 0, 0, 0);*/


            pictureBox1.Image = bitmap;
        }

        private Graphics g;
        private Bitmap bitmap;
        Point p0;


    }

    public class MyPointNode
    {
        public Point point;
        public MyPointNode up;
        public MyPointNode right;
        public MyPointNode down;
        public MyPointNode left;
    }

    public class MySquarePolygon
    {
        public List<MyPointNode> nodes;

        public MySquarePolygon()
        {
            nodes = new List<MyPointNode>();
        }

        public MySquarePolygon(Point point) // левая верхняя точка
        {
            Point p2 = new Point(point.X + 50, point.Y);
            Point p3 = new Point(point.X + 50, point.Y + 50);
            Point p4 = new Point(point.X, point.Y + 50);

            MyPointNode n1 = new MyPointNode() { point = point };
            MyPointNode n2 = new MyPointNode() { point = p2 };
            MyPointNode n3 = new MyPointNode() { point = p3 };
            MyPointNode n4 = new MyPointNode() { point = p4 };

            nodes = new List<MyPointNode>() { n1,n2,n3,n4};

            n1.right = n2;
            n2.left = n1;
            n2.down = n3;
            n3.up = n2;
            n3.left = n4;
            n4.right = n3;
            n4.up = n1;
        }

        public MySquarePolygon(MySquarePolygon polygon)
        {
            MySquarePolygon newPolygon = new MySquarePolygon();


        }
    }

    public class MyPolygonGen
    {
        public PictureBox pictureBox;
        public Bitmap bitmap;
        public Graphics g;

        private int count;

        public MyPolygonGen(PictureBox pictureBox, Bitmap bitmap, Graphics g)
        {
            this.pictureBox = pictureBox;
            this.bitmap = bitmap;
            this.g = g;
            count = 2;
        }

        public object G(MyPolygon p, int n1, int n2, int n3)
        {
            if (n1 > 3 && n2 > 3 && n3 > 3)
            {
                return null;
            }
            else
            {
                if (n1 <= 3)
                {
                    MyPolygon p2 = new MyPolygon(new Point(p.points[0].X - (p.p0.X - p.points[1].X), p.points[1].Y), 50, 3, 60);

                    foreach (var p1 in p2.points)
                    {
                        g.DrawLine(Pens.Black, new Point(p.points[0].X - (p.p0.X - p.points[1].X), p.points[1].Y), p1);
                        g.FillEllipse(Brushes.Green, p1.X - 3, p1.Y - 3, 6, 6);
                    }
                    G(p2, n1 + 1, n2, n3);
                }

                if (n2 <= 3)
                {
                    MyPolygon p3 = new MyPolygon(new Point(p.points[0].X - (p.p0.X - p.points[1].X), p.points[2].Y), 50, 3, 60);

                    foreach (var p1 in p3.points)
                    {
                        g.DrawLine(Pens.Black, new Point(p.points[0].X - (p.p0.X - p.points[1].X), p.points[2].Y), p1);
                        g.FillEllipse(Brushes.Green, p1.X - 3, p1.Y - 3, 6, 6);
                    }
                    G(p3, n1, n2 + 1, n3);
                }

                if (n3 <= 3)
                {
                    MyPolygon p4 = new MyPolygon(new Point(p.points[1].X - (p.p0.X - p.points[1].X), p.points[0].Y), 50, 3, 60);

                    foreach (var p1 in p4.points)
                    {
                        g.DrawLine(Pens.Black, new Point(p.points[1].X - (p.p0.X - p.points[1].X), p.points[0].Y), p1);
                        g.FillEllipse(Brushes.Green, p1.X - 3, p1.Y - 3, 6, 6);
                    }
                    G(p4, n1, n2, n3 + 1);
                }

                return null;
            }

        }
    }

    public class MyPolygon // организовать связь центральных точек кластера через двусвязный список?
    {
        public Point p0;
        private int length;
        private int n;
        public List<Point> points;

        public MyPolygon(Point p0, int length, int n, int startAngle)
        {
            this.n = 3;
            this.length = 50;
            this.p0 = p0;

            points = new List<Point>();

            int angle = 360 / this.n;
            int curAngle = 0 + startAngle;

            for (int i = 0; i < 3; i++)
            {
                Point p1 = new Point(); 
                p1.X = p0.X + Convert.ToInt32(length * Math.Cos(curAngle * Math.PI / 180));
                p1.Y = p0.Y - Convert.ToInt32(length * Math.Sin(curAngle * Math.PI / 180));

                points.Add(p1);
                curAngle += angle;
            }
        }

        public void FillPolygon(PictureBox pictureBox, Bitmap bitmap, Graphics g, Color color) 
        {
            g.FillPolygon(Brushes.Red, new Point[] { p0, points[0], points[1] });
            g.FillPolygon(Brushes.Blue, new Point[] { p0, points[1], points[2] });
            g.FillPolygon(Brushes.Green, new Point[] { p0, points[2], points[0] });

            pictureBox.Image = bitmap;
        }
    }

    
}
