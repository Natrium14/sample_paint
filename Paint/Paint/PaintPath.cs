using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Paint
{
    public class PaintPero: PaintBase
    {
        public PaintPero(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {
            this.bitmap = bitmap;
            this.graphics = graphics;
            this.pictureBox = pictureBox;
            points = new List<Point>();
            bytes = new List<byte>();
            PenWidth = 40.0f;
            ca1 = 0.0f;
            ca2 = 0.5f;
            ca3 = 0.5f;
            ca4 = 1.0f;
            PenInit(Brushes.Black);
        }

        private List<Point> points;
        private List<byte> bytes;
        private GraphicsPath graphicsPath;
        public Pen myPen { get; set; }
        public float PenWidth { get; set; }
        public float ca1 { get; set; }
        public float ca2 { get; set; }
        public float ca3 { get; set; }
        public float ca4 { get; set; }

        private void PenInit(Brush brush)
        {
            myPen = new Pen(brush);
            myPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            myPen.CompoundArray = new float[] {ca1, ca2, ca3, ca4 };
            myPen.Width = PenWidth;
            //myPen.MiterLimit = 1.0f;
            //myPen.DashStyle = DashStyle.Solid;
            //myPen.StartCap = LineCap.NoAnchor;
            //myPen.EndCap = LineCap.Triangle;
        }

        public void SetStartPoint(int x, int y, Brush brush, int brushSize)
        {
            PenWidth = brushSize;
            PenInit(brush);
            points.Clear();
            bytes.Clear();
            Point newPoint = new Point(x, y);
            points.Add(newPoint);
            bytes.Add(1);
        }

        public void AddPathPoint(int x, int y)
        {
            Point newPoint = new Point(x, y);
            points.Add(newPoint);
            bytes.Add(1);
            graphicsPath = new GraphicsPath(points.ToArray(), bytes.ToArray());
            graphics.DrawPath(myPen, graphicsPath);
        }

        public void DrawPath()
        {
            graphicsPath = new GraphicsPath(points.ToArray(), bytes.ToArray());
            graphics.DrawPath(myPen, graphicsPath);
        }
    }
}
