using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint.TestIzometria
{

    class MyPanel
    {
        public MyPoint3D point3D;
        public Panel panel;
        public List<MyPanelLink> links;

        public MyPanel(Panel panel, MyPoint3D point3D)
        {
            this.point3D = point3D;
            this.panel = panel;
            links = new List<MyPanelLink>();
        }
    }
}
