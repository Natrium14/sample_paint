using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint.Test3D
{
    interface IMyFigure
    {
        List<MyPoint> myPoints { get; set; }
        void FillPoly(Graphics g, SolidBrush brush);
        void PovernutProtiv(double angleChange);
        void PovernutPo(double angleChange);
        void PovernutVniz(double angleChange);
        void PovernutVverh(double angleChange);
    }
}
