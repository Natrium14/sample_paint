using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint.Test3D
{
    class MyFigure : IMyFigure
    {
        public int angle0;
        public double angleProtiv, angleVniz, anglePo, angleVverh;
        public List<MyPoint> myPoints
        { get; set; }
        public virtual void FillPoly(Graphics g, SolidBrush brush)
        {
            throw new NotImplementedException();
        }

        public void PovernutProtiv(double angleChange)
        {
            angleProtiv += angleChange;
            for (int i = 0; i < myPoints.Count; i++)
            {
                var a1 = Math.PI * (angleProtiv + myPoints[i].angleX) / 180.0;
                var b1 = Math.Cos(a1);
                int k1 = Convert.ToInt32(myPoints[i].lengthX * b1);
                myPoints[i].point.X = myPoints[i].point0.X + k1;
            }
        }
        public void PovernutPo(double angleChange)
        {
            anglePo += angleChange;
            for (int i = 0; i < myPoints.Count; i++)
            {
                var a1 = Math.PI * (anglePo + myPoints[i].angleX) / 180.0;
                var b1 = Math.Cos(a1);
                int k1 = Convert.ToInt32(myPoints[i].lengthX * b1);
                myPoints[i].point.X = myPoints[i].point0.X - k1;
            }
        }
        public void PovernutVniz(double angleChange)
        {
            angleVniz += angleChange;
            for (int i = 0; i < myPoints.Count; i++)
            {
                var a1 = Math.PI * (angleVniz + myPoints[i].angleY) / 180.0;
                var b1 = Math.Cos(a1);
                int k1 = Convert.ToInt32(myPoints[i].lengthY * b1);
                myPoints[i].point.Y = myPoints[i].point0.Y + k1;
            }
        }
        public void PovernutVverh(double angleChange)
        {
            angleVverh += angleChange;
            for (int i = 0; i < myPoints.Count; i++)
            {
                var a1 = Math.PI * (angleVverh + myPoints[i].angleY) / 180.0;
                var b1 = Math.Cos(a1);
                int k1 = Convert.ToInt32(myPoints[i].lengthY * b1);
                myPoints[i].point.Y = myPoints[i].point0.Y - k1;
            }
        }
    }
}
