using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.TestFractal
{
    public partial class FormTestFractal : Form
    {
        public FormTestFractal()
        {
            InitializeComponent();
        }

        private Graphics g;
        private Bitmap bitmap;

        private void button1_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);

            //Выбираем перо "myPen" черного цвета Black
            //толщиной в 1 пиксель:
            Pen myPen = new Pen(Color.Black, 0.5f);
            //Объявляем объект "g" класса Graphics и предоставляем
            //ему возможность рисования на pictureBox1:
           // Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            //вызываем функцию рисования фрактала
            DrawFractal(pictureBox1.Width, pictureBox1.Height, myPen);
        }

        //функция зарисовки фрактала
        public void DrawFractal(int w, int h, Pen pen)
        {
            // при каждой итерации, вычисляется znew = zold² + С

            // вещественная  и мнимая части постоянной C
            double cRe, cIm;
            // вещественная и мнимая части старой и новой
            double newRe, newIm, oldRe, oldIm;
            // Можно увеличивать и изменять положение
            double zoom = 1, moveX = 0, moveY = 0;
            //Определяем после какого числа итераций функция должна прекратить свою работу
            int maxIterations = Convert.ToInt32(numericUpDown1.Value);

            //выбираем несколько значений константы С, это определяет форму фрактала         Жюлиа
            cRe = -0.70176;
            cIm = -0.3842;

            Random r = new Random();
            cRe += Convert.ToDouble(r.Next(-900, 900)) / 20000;
            cIm += Convert.ToDouble(r.Next(-700, 700)) / 20000;

            //"перебираем" каждый пиксель
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                {
                    //вычисляется реальная и мнимая части числа z
                    //на основе расположения пикселей,масштабирования и значения позиции
                    newRe = 1.5 * (x - w / 2) / (0.5 * zoom * w) + moveX;
                    newIm = (y - h / 2) / (0.5 * zoom * h) + moveY;

                    //i представляет собой число итераций 
                    int i;

                    //начинается процесс итерации
                    for (i = 0; i < maxIterations; i++)
                    {

                        //Запоминаем значение предыдущей итерации
                        oldRe = newRe;
                        oldIm = newIm;

                        // в текущей итерации вычисляются действительная и мнимая части 
                        newRe = oldRe * oldRe - oldIm * oldIm + cRe;
                        newIm = 2 * oldRe * oldIm + cIm;

                        // если точка находится вне круга с радиусом 2 - прерываемся
                        if ((newRe * newRe + newIm * newIm) > 4) break;
                    }

                    //определяем цвета
                    pen.Color = Color.FromArgb(255, 0, (i * 3) % 255, (i * 3) % 255); //9
                    //рисуем пиксель
                    g.DrawEllipse(pen, x, y, 1, 1);
                }

            pictureBox1.Image = bitmap;
        }

        private void FormTestFractal_Load(object sender, EventArgs e)
        {
        }

    }
}
