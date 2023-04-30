using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Paint.TestIzometria
{
    public partial class FormTestIzometria : Form
    {
        public FormTestIzometria()
        {
            InitializeComponent();

            bitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g1 = Graphics.FromImage(bitmap1);
            bitmap2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g2 = Graphics.FromImage(bitmap2);
            bitmap3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            g3 = Graphics.FromImage(bitmap3);


            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            g = Graphics.FromImage(bitmap);
            p0 = new Point(pictureBox.Width / 2, pictureBox.Height - 20);

            myPanels = new List<MyPanel>();
            currentPanel = new MyPanel(new Panel(), new MyPoint3D());





            //CreateParaboloid();

        }
        
        Graphics g, g1, g2, g3;
        Bitmap bitmap, bitmap1, bitmap2, bitmap3;

        Point p0;

        List<MyPanel> myPanels;
        MyPanel currentPanel;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(Color.White);
            g.DrawLine(new Pen(Color.Black, 0.3f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 
                p0, 
                new Point(pictureBox.Width, Convert.ToInt32(p0.Y - Math.Sin(30*Math.PI/180) * (pictureBox.Width / 2) ))); // x

            g.DrawLine(new Pen(Color.Black, 0.3f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 
                p0, 
                new Point(0, Convert.ToInt32(p0.Y - Math.Sin(30*Math.PI/180) * (pictureBox.Width / 2) ))); // z

            g.DrawLine(new Pen(Color.Black, 0.3f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 
                p0, 
                new Point(p0.X, 0)); // y

            foreach (var p in myPanels)
            {
                if (p.links.Count > 0)
                {
                    foreach (var l in p.links)
                    {
                        var point1 = new Point(myPanels.FirstOrDefault(x=>x.panel.Name == l.name1).panel.Location.X + 5,
                                                myPanels.FirstOrDefault(x => x.panel.Name == l.name1).panel.Location.Y + 5);
                        var point2 = new Point(myPanels.FirstOrDefault(x => x.panel.Name == l.name2).panel.Location.X + 5,
                                                myPanels.FirstOrDefault(x => x.panel.Name == l.name2).panel.Location.Y + 5);
                        g.DrawLine(Pens.Black, point1, point2);
                    }
                }
            }

            pictureBox.Image = bitmap;

            
            g1.Clear(Color.White);
            foreach (var p in myPanels)
            {
                var a = p.point3D.GetXYPoint();
                
                if (currentPanel.panel.Name == p.panel.Name)
                {
                    g1.FillRectangle(Brushes.Blue, a.X - 5, a.Y - 5, 10, 10);
                }
                else
                {
                    g1.FillRectangle(Brushes.Black, a.X - 5, a.Y - 5, 10, 10);
                }

                if (p.links.Count > 0)
                {
                    foreach (var l in p.links)
                    {

                        var point1 = new Point(myPanels.First(x => x.panel.Name == l.name1).point3D.GetXYPoint().X, myPanels.First(x => x.panel.Name == l.name1).point3D.GetXYPoint().Y);
                        var point2 = new Point(myPanels.First(x => x.panel.Name == l.name2).point3D.GetXYPoint().X, myPanels.First(x => x.panel.Name == l.name2).point3D.GetXYPoint().Y);
                        g1.DrawLine(Pens.Black, point1, point2);
                    }
                }
            }
            pictureBox1.Image = bitmap1;

            g2.Clear(Color.White);
            foreach (var p in myPanels)
            {
                var a = p.point3D.GetZYPoint();
                if (currentPanel.panel.Name == p.panel.Name)
                {
                    g2.FillRectangle(Brushes.Blue, a.X - 5, a.Y - 5, 10, 10);
                }
                else
                {
                    g2.FillRectangle(Brushes.Black, a.X - 5, a.Y - 5, 10, 10);
                }
                if (p.links.Count > 0)
                {
                    foreach (var l in p.links)
                    {

                        var point1 = new Point(myPanels.First(x => x.panel.Name == l.name1).point3D.GetZYPoint().X, myPanels.First(x => x.panel.Name == l.name1).point3D.GetZYPoint().Y);
                        var point2 = new Point(myPanels.First(x => x.panel.Name == l.name2).point3D.GetZYPoint().X, myPanels.First(x => x.panel.Name == l.name2).point3D.GetZYPoint().Y);
                        g2.DrawLine(Pens.Black, point1, point2);
                    }
                }
            }
            pictureBox2.Image = bitmap2;

            g3.Clear(Color.White);
            foreach (var p in myPanels)
            {
                var a = p.point3D.GetXZPoint();
                if (currentPanel.panel.Name == p.panel.Name)
                {
                    g3.FillRectangle(Brushes.Blue, a.X - 5, a.Y - 5, 10, 10);
                }
                else
                {
                    g3.FillRectangle(Brushes.Black, a.X - 5, a.Y - 5, 10, 10);
                }
                if (p.links.Count > 0)
                {
                    foreach (var l in p.links)
                    {

                        var point1 = new Point(myPanels.First(x => x.panel.Name == l.name1).point3D.GetXZPoint().X, myPanels.First(x => x.panel.Name == l.name1).point3D.GetXZPoint().Y);
                        var point2 = new Point(myPanels.First(x => x.panel.Name == l.name2).point3D.GetXZPoint().X, myPanels.First(x => x.panel.Name == l.name2).point3D.GetXZPoint().Y);
                        g3.DrawLine(Pens.Black, point1, point2);
                    }
                }
            }
            pictureBox3.Image = bitmap3;

        }

        

        private void button_Click(object sender, EventArgs e)
        {
            Panel b = sender as Panel;
            b.BackColor = Color.Blue;

            foreach (var bb in myPanels)
            {
                if (bb.panel.Name != b.Name)
                {
                    bb.panel.BackColor = Color.Black;
                    bb.panel.Refresh();
                }
            }

            currentPanel.panel = b;
            b.Refresh();

            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
            }

            foreach (var p in myPanels)
            {
                if (p.panel.Name == currentPanel.panel.Name)
                {
                    numericUpDownX.Value = p.point3D.x;
                    numericUpDownY.Value = p.point3D.y;
                    numericUpDownZ.Value = p.point3D.z;
                }
            }
        }

        /*
        private void CreateParaboloid()
        {
            int x=0, y=0, z=0;
            for (int i = 0; i < 200; i++)
            {
                y = (x * x / 10) - (z * z / 12);
                
                MyPoint3D location = new MyPoint3D(x, y, z, p0);
                Panel newPanel = new Panel();
                newPanel.Name = "but" + myPanels.Count;
                newPanel.Width = 2;
                newPanel.Height = 2;
                newPanel.Location = new Point(location.point.X - newPanel.Width / 2, location.point.Y - newPanel.Height / 2);
                newPanel.Click += button_Click;
                newPanel.BackColor = Color.Black;
                this.Controls.Add(newPanel);
                newPanel.Parent = pictureBox;

                myPanels.Add(new MyPanel(newPanel, location));

                Thread.Sleep(10);
                x++;
                z++;
            }
        }*/

        private void CreatePanel(int x, int y, int z)
        {
            MyPoint3D location = new MyPoint3D(x, y, z, p0);

            Panel newPanel = new Panel();
            newPanel.Name = "but" + myPanels.Count;
            newPanel.Width = 10;
            newPanel.Height = 10;
            newPanel.Location = new Point(location.point.X - newPanel.Width / 2, location.point.Y - newPanel.Height / 2);
            newPanel.Click += button_Click;
            newPanel.BackColor = Color.Black;
            this.Controls.Add(newPanel);
            newPanel.Parent = pictureBox;

            myPanels.Add(new MyPanel(newPanel, location));

            Thread.Sleep(100);
        }


        private void CreateLink(ref MyPanel panel1, ref MyPanel panel2)
        {
            MyPanelLink newLink = new MyPanelLink();
            newLink.name1 = panel1.panel.Name;
            newLink.name2 = panel2.panel.Name;
            panel1.links.Add(newLink);
            panel2.links.Add(newLink);              
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(numericUpDownX.Value);
            int y = Convert.ToInt32(numericUpDownY.Value);
            int z = Convert.ToInt32(numericUpDownZ.Value);
            MyPoint3D location = new MyPoint3D(x,y,z,p0);

            Panel newPanel = new Panel();
            newPanel.Name = "but" + myPanels.Count;
            newPanel.Width = 10;
            newPanel.Height = 10;
            newPanel.Location = new Point(location.point.X - newPanel.Width / 2, location.point.Y - newPanel.Height/2);
            newPanel.Click += button_Click;
            newPanel.BackColor = Color.Black;
            this.Controls.Add(newPanel);
            newPanel.Parent = pictureBox;

            myPanels.Add(new MyPanel(newPanel, location));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var panel in myPanels)
            {
                if (panel.links.Exists(x => x.name1 == currentPanel.panel.Name || x.name2 == currentPanel.panel.Name))
                {
                    panel.links.Clear();
                }
            }

            //var cur = myPanels.First(x => x.panel.Name == currentPanel.panel.Name);

            myPanels.Remove(myPanels.First(x => x.panel.Name == currentPanel.panel.Name));
            currentPanel.panel.Dispose();
            currentPanel = new MyPanel(new Panel(), new MyPoint3D());
            pictureBox.Invalidate();
        }

        MyPanelLink link;

        private void button3_Click(object sender, EventArgs e)
        {
            if (link == null)
            {
                link = new MyPanelLink();
                if (currentPanel.panel.Name != "")
                {
                    link.name1 = currentPanel.panel.Name;
                    link.name2 = currentPanel.panel.Name;
                    foreach (var bb in myPanels)
                    {
                        if (bb.panel.Name == currentPanel.panel.Name)
                        {
                            bb.links.Add(link);
                        }
                    }
                    progressBar1.Value = 50;
                }
            }
            else
            {
                if (link.name1 != "")
                {
                    link.name2 = currentPanel.panel.Name;
                    foreach (var bb in myPanels)
                    {
                        if (bb.panel.Name == currentPanel.panel.Name)
                        {
                            bb.links.Add(link);
                        }
                    }

                    progressBar1.Value = 100;
                    link = null;
                    pictureBox.Invalidate();
                }
            }
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (var p in myPanels)
                {
                    if (p.panel.Name == currentPanel.panel.Name)
                    {
                        p.point3D.ChangeX(Convert.ToInt32(numericUpDownX.Value));
                        p.panel.Location = new Point(p.point3D.point.X - 5, p.point3D.point.Y - 5);
                    }
                }

                pictureBox.Invalidate();
            }
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (var p in myPanels)
                {
                    if (p.panel.Name == currentPanel.panel.Name)
                    {
                        p.point3D.ChangeY(Convert.ToInt32(numericUpDownY.Value));
                        p.panel.Location = new Point(p.point3D.point.X - 5, p.point3D.point.Y - 5);
                    }
                }

                pictureBox.Invalidate();
            }
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (var p in myPanels)
                {
                    if (p.panel.Name == currentPanel.panel.Name)
                    {
                        p.point3D.ChangeZ(Convert.ToInt32(numericUpDownZ.Value));
                        p.panel.Location = new Point(p.point3D.point.X - 5, p.point3D.point.Y - 5);
                    }
                }

                pictureBox.Invalidate();
            }
        }

    }
}
