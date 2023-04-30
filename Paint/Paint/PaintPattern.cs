using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
    public class PaintPattern : PaintBase, ICloneable
    {
        public PaintPattern(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {
            this.bitmap = bitmap;
            this.graphics = graphics;
            this.pictureBox = pictureBox;
            points = new List<Point>();
            bytes = new List<byte>();
        }

        public List<Point> points { get; set; }
        public List<byte> bytes;
        private GraphicsPath graphicsPath;

        public GraphicsPath GetPath()
        {
            if (graphicsPath != null)
                return graphicsPath;
            else
                return new GraphicsPath(points.ToArray(), bytes.ToArray());
        }

        public void SetStartPoint(int x, int y)
        {
            Point newPoint = new Point(x, y);
            points.Add(newPoint);
            bytes.Add(0);
        }

        public void AddPathPoint(int x, int y)
        {
            Point newPoint = new Point(x, y);
            points.Add(newPoint);
            bytes.Add(1);
            graphicsPath = new GraphicsPath(points.ToArray(), bytes.ToArray());
            graphics.DrawPath(Pens.Black, graphicsPath);
            pictureBox.Image = bitmap;
        }

        public void SavePath()
        {
            graphicsPath = new GraphicsPath(points.ToArray(), bytes.ToArray()); 
            graphics.DrawPath(Pens.Black, graphicsPath);
            pictureBox.Image = bitmap;
        }

        public void ClearPath()
        {
            points.Clear();
            bytes.Clear();

            for (int i = 1; i < pictureBox.Width - 1; i++)
                for (int j = 1; j < pictureBox.Height - 1; j++)
                {
                    bitmap.SetPixel(i, j, Color.White);
                }

            pictureBox.Image = bitmap;
        }

        public object Clone()
        {
            return new PaintPattern(pictureBox, bitmap, graphics)
            {
                graphicsPath = this.graphicsPath,
                points = this.points,
                bytes = this.bytes
            };
        }
    }
}
