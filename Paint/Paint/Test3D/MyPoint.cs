using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint.Test3D
{
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
}
