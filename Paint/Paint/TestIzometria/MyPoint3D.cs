using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint.TestIzometria
{
    class MyPoint3D
    {
        public int x;
        public int y;
        public int z;
        public Point point;
        private Point p0;

        public MyPoint3D() { }

        public MyPoint3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public MyPoint3D(int x, int y, int z, Point p0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.p0 = p0;
            point = setPoint();
        }

        public void ChangeX(int x)
        {
            this.x = x;
            point = setPoint();
        }
        public void ChangeY(int y)
        {
            this.y = y;
            point = setPoint();
        }
        public void ChangeZ(int z)
        {
            this.z = z;
            point = setPoint();
        }

        private Point setPoint()
        {
            Point newPoint = new Point();

            double x1 = x * Math.Cos(30 * Math.PI / 180); // x 
            double x2 = z * Math.Cos(30 * Math.PI / 180); // z 
            int x_ = Convert.ToInt32(x1 - x2 + p0.X);
            newPoint.X = x_;

            double y1 = x * Math.Sin(30 * Math.PI / 180); // 
            double y2 = z * Math.Sin(30 * Math.PI / 180); // 
            int y_ = Convert.ToInt32(p0.Y - (y1 + y2));
            newPoint.Y = y_;

            newPoint.Y = newPoint.Y - y;

            return newPoint;
        }

        public Point GetXYPoint()
        {
            Point newPoint = new Point(this.x, this.y);
            return newPoint;
        }

        public Point GetZYPoint()
        {
            Point newPoint = new Point(this.z, this.y);
            return newPoint;
        }
        public Point GetXZPoint()
        {
            Point newPoint = new Point(this.x, this.z);
            return newPoint;
        }

    } 
}
