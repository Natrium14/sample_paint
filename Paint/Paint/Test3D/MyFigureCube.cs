using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint.Test3D
{
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

            myPoints.Add(new MyPoint(p1, 1, p0, 315, 315, new List<MyLink>()));
            myPoints.Add(new MyPoint(p2, 2, p0, 225, 315, new List<MyLink>() 
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
            myPoints.Add(new MyPoint(p5, 5, p0, 315, 45, new List<MyLink>()
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

            angle0 = 0;
            angleProtiv = angle0;
            angleVniz = angle0;
            anglePo = angle0;
            angleVverh = angle0;
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
}
