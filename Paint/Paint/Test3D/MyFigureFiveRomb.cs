using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint.Test3D
{
    class MyFigureFiveRomb : MyFigure
    {
        public MyFigureFiveRomb(Point p0)
        {
            Point p1 = new Point(p0.X, p0.Y - 200);
            Point p2 = new Point(p0.X - 200, p0.Y + 100);
            Point p3 = new Point(p0.X - 160, p0.Y + 100);
            Point p4 = new Point(p0.X + 200, p0.Y + 100);
            Point p5 = new Point(p0.X + 200, p0.Y + 100);
            Point p6 = new Point(p0.X - 160, p0.Y + 100);
            Point p7 = new Point(p0.X, p0.Y + 200);

            myPoints = new List<MyPoint>();
            myPoints.Add(new MyPoint(p1, 1, p0, 0, 270, new List<MyLink>()));
            myPoints.Add(new MyPoint(p2, 2, p0, 0, 90, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 2
                    }
                }));
            myPoints.Add(new MyPoint(p3, 3, p0, 288, 18, new List<MyLink>() 
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
            myPoints.Add(new MyPoint(p4, 4, p0, 216, 306, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 4
                    },
                    new MyLink(){
                        number1 = 3,
                        number2 = 4
                    }
                }));
            myPoints.Add(new MyPoint(p5, 5, p0, 144, 234, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 5
                    },
                    new MyLink(){
                        number1 = 4,
                        number2 = 5
                    }
                }));
            myPoints.Add(new MyPoint(p6, 6, p0, 72, 162, new List<MyLink>() 
                { 
                    new MyLink(){
                        number1 = 1,
                        number2 = 6
                    },
                    new MyLink(){
                        number1 = 5,
                        number2 = 6
                    },
                    new MyLink(){
                        number1 = 6,
                        number2 = 2
                    }
                }));
            myPoints.Add(new MyPoint(p7, 7, p0, 0, 90, new List<MyLink>()
                {
                    new MyLink(){
                        number1 = 2,
                        number2 = 7
                    },
                    new MyLink(){
                        number1 = 3,
                        number2 = 7
                    },
                    new MyLink(){
                        number1 = 4,
                        number2 = 7
                    },
                    new MyLink(){
                        number1 = 5,
                        number2 = 7
                    },
                    new MyLink(){
                        number1 = 6,
                        number2 = 7
                    }
                }
            ));

            angle0 = 0;
            angleProtiv = angle0;
            angleVniz = angle0;
            anglePo = angle0;
            angleVverh = angle0;
        }
        public override void FillPoly(Graphics g, SolidBrush brush)
        {
            g.FillPolygon(new SolidBrush(Color.FromArgb(70, 220, 150, 150)), new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 1).point,
                    myPoints.FirstOrDefault(x=>x.number == 2).point,
                    myPoints.FirstOrDefault(x=>x.number == 3).point
                });

            g.FillPolygon(new SolidBrush(Color.FromArgb(70, 250, 120, 150)), new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 1).point,
                    myPoints.FirstOrDefault(x=>x.number == 3).point,
                    myPoints.FirstOrDefault(x=>x.number == 4).point
                });

            g.FillPolygon(new SolidBrush(Color.FromArgb(70, 250, 150, 120)), new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 1).point,
                    myPoints.FirstOrDefault(x=>x.number == 4).point,
                    myPoints.FirstOrDefault(x=>x.number == 5).point
                });

            g.FillPolygon(new SolidBrush(Color.FromArgb(70, 250, 150, 150)), new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 1).point,
                    myPoints.FirstOrDefault(x=>x.number == 5).point,
                    myPoints.FirstOrDefault(x=>x.number == 6).point
                });

            g.FillPolygon(new SolidBrush(Color.FromArgb(70, 120, 150, 250)), new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 7).point,
                    myPoints.FirstOrDefault(x=>x.number == 2).point,
                    myPoints.FirstOrDefault(x=>x.number == 3).point
                });

            g.FillPolygon(new SolidBrush(Color.FromArgb(70, 150, 120, 250)), new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 7).point,
                    myPoints.FirstOrDefault(x=>x.number == 3).point,
                    myPoints.FirstOrDefault(x=>x.number == 4).point
                });

            g.FillPolygon(new SolidBrush(Color.FromArgb(70, 150, 150, 220)), new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 7).point,
                    myPoints.FirstOrDefault(x=>x.number == 4).point,
                    myPoints.FirstOrDefault(x=>x.number == 5).point
                });

            g.FillPolygon(new SolidBrush(Color.FromArgb(70, 150, 150, 250)), new Point[]{
                    myPoints.FirstOrDefault(x=>x.number == 7).point,
                    myPoints.FirstOrDefault(x=>x.number == 5).point,
                    myPoints.FirstOrDefault(x=>x.number == 6).point
                });

        }
    }
}
