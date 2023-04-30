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
    public partial class PaintBrushPattern : Form
    {
        public PaintBrushPattern()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
            g.Clip = new Region(new Rectangle(1,1, 98, 98));

            paintPattern = new PaintPattern(pictureBox1, bitmap, g);
            savedPatterns = new List<PaintPattern>();
        }
        
        private Graphics g;
        private Bitmap bitmap;
        public PaintPattern paintPattern { get; set; }
        bool toolDown = false;
        List<PaintPattern> savedPatterns;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            paintPattern.SetStartPoint(e.X, e.Y);

            if (toolDown == false)
            {
                toolDown = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (toolDown == true)
            {
                paintPattern.AddPathPoint(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            paintPattern.SavePath();

            if (toolDown == true)
            {
                toolDown = false;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            paintPattern.ClearPath();
        }

        private void SavePattern()
        {
            var a = (PaintPattern) paintPattern.Clone();
            savedPatterns.Add(a);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SavePattern();
        }
    }
}
