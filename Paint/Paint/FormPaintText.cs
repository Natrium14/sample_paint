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
    public partial class FormPaintText : Form
    {
        public FormPaintText()
        {
            InitializeComponent();
            font = new Font("Arial", 16);
        }

        internal Font font;

        private void button2_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            font = fontDialog1.Font;
        }
    }
}
