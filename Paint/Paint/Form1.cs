using Paint.TestFractal;
using Paint.TestIzometria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            backColor = Color.White;

            bitmapInit(Convert.ToInt32(textBoxCanvasWidth.Text), Convert.ToInt32(textBoxCanvasHeight.Text));

            panelBackColor.BackColor = backColor;
            brushErazer = new SolidBrush(backColor);

            currentColor = Color.FromArgb(255, Color.Black);
            brush = new SolidBrush(currentColor);
            panelCurrentColor.BackColor = currentColor;

            colorPanelBitmap = new Bitmap(pictureBoxColorPicker.Width, pictureBoxColorPicker.Height);
            colorPanelGraphics = Graphics.FromImage(colorPanelBitmap);
            pictureBoxColorPicker.Image = colorPanelBitmap;
            paintColorPicker = new PaintColorPicker(pictureBoxColorPicker, colorPanelBitmap);
            paintColorPicker.InitColorPanel4();

            PaintToolsInit();
            formPenPattern = new FormPenPattern(ref paintPath);
            formBrushPattern = new PaintBrushPattern();

            originator = new Originator();
            Bitmap bitmap1 = (Bitmap)bitmap.Clone();
            originator.State = bitmap1;
            caretaker = new Caretaker() { Action="Создание полотна", Number = 0 };
            caretaker.Memento = originator.CreateMemento();
            
            caretakers = new List<Caretaker>();
            caretakers.Add(caretaker);
            listBoxHistory.Items.Add(caretaker);
            listBoxHistory.DisplayMember = "Display";

        }

        Bitmap bitmap;
        Bitmap colorPanelBitmap;
        Graphics graphics;
        Graphics colorPanelGraphics;
        Region baseRegion; // область рисования весь битмап, для отмены выделения

        PaintColorPicker paintColorPicker;
        Color currentColor;
        Color backColor;

        SolidBrush brush;
        SolidBrush brushErazer;
        PaintPipette pipette;
        PaintBalon paintBalon;
        PaintLine paintline;
        PaintCircle paintCircle;
        PaintSquare paitSquare;
        PaintRandomPolygon paintRandPoly;
        PaintText paintText;
        PaintFill paintFill;
        PaintFillSmooth paintFillSmooth;
        PaintFilterColor paintFilterColor;
        PaintFilterNoise paintFilterNoise;
        PaintFilterSmooth paintFilterSmooth;
        PaintGradient gradient;
        PaintBrushXOR brushXOR;
        PaintCloud paintCloud;
        PaintWave paintWave;
        PaintSelection paintSelection;
        PaintBalonFilter paintBalonFilter;
        PaintPero paintPath;

        FormPenPattern formPenPattern;
        PaintBrushPattern formBrushPattern;

        Originator originator;
        Caretaker caretaker;
        List<Caretaker> caretakers;
        
        int brushSize = 5;

        bool toolDown = false;
        bool changeCurrentColorByClick = true;

        private void pictureBoxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButtonBrushCircle.Checked)
            {
                graphics.FillEllipse(brush, e.X - brushSize / 2, e.Y - brushSize / 2, brushSize, brushSize);
                AddToHistory("Кисть круг");
            }
            if (radioButtonBrushSquare.Checked)
            {
                graphics.FillRectangle(brush, e.X - brushSize / 2, e.Y - brushSize / 2, brushSize, brushSize);
                AddToHistory("Кисть квадрат");
            }
            if (radioButtonPipette.Checked)
            {
                pipette.GetColor(e.X, e.Y, ref bitmap, ref currentColor);
                brush.Color = currentColor;
                panelCurrentColor.BackColor = currentColor;
            }
            if (radioButtonFill.Checked)
            {
                //graphics.DrawRectangle(new Pen(Color.FromArgb(254, 254, 254)), 0, 0, pictureBoxCanvas.Width - 2, pictureBoxCanvas.Height - 2);
                if (checkBoxFillSmooth.Checked)
                    paintFillSmooth.FillSmooth(e.X, e.Y, currentColor);
                else
                    paintFill.Fill(e.X, e.Y, currentColor);

                AddToHistory("Заливочка");
            }
            if (radioButtonBrushErazer.Checked)
            {
                graphics.FillRectangle(brushErazer, e.X - brushSize / 2, e.Y - brushSize / 2, brushSize, brushSize);
                AddToHistory("Кисть cтерка");
            }
            if (radioButtonBalon.Checked)
            {
                if (checkBoxBalonNoise.Checked && checkBoxBalonNoiseCanvas.Checked)
                    paintBalonFilter.FillNoise(e.X, e.Y, brushSize);
                else
                {
                    if (checkBoxBalonNoise.Checked)
                        paintBalonFilter.FillNoiseColor(e.X, e.Y, currentColor, brushSize);
                    else
                        paintBalon.Fill(e.X, e.Y, currentColor, brushSize);
                }
                AddToHistory("Балон");
            }
            if (radioButtonLine.Checked)
            {
                paintline.SetStartPoint(e.X, e.Y);
            }
            if (radioButtonCircle.Checked)
            {
                paintCircle.SetStartPoint(e.X, e.Y);
            }
            if (radioButtonSquare.Checked)
            {
                paitSquare.SetStartPoint(e.X, e.Y);
            }
            if (radioButton1.Checked) //remove
            {
                paintRandPoly.SetStartPoint(e.X, e.Y);
            }
            if (radioButtonText.Checked)
            {
                paintText.SetText(e.X, e.Y, brush);
                AddToHistory("Текст");
            }
            if (radioButtonGrad.Checked)
            {
                gradient.SetStartPoint(e.X, e.Y);
            }
            if (radioButtonBrushXOR.Checked)
            {
                if (checkBoxBrushXor.Checked)
                    brushXOR.PaintDrawSquareXOR(e.X, e.Y, Color.White, brushSize);
                else
                    brushXOR.PaintDrawSquareXOR(e.X, e.Y, currentColor, brushSize);
                AddToHistory("Кисть XOR");
            }
            if (radioButtonWave.Checked)
            {
                paintWave.SetStartPoint(e.X, e.Y);
            }
            if (radioButtonBlurBrush.Checked)
            {
                paintBalonFilter.FillBlur(e.X, e.Y, brushSize);
                AddToHistory("Размытие");
            }
            if (radioButtonGray.Checked)
            {
                paintBalonFilter.FillGray(e.X, e.Y, brushSize);
                AddToHistory("Обесцвечивание");
            }
            if (radioButtonSelectionSquare.Checked)
            {
                paintSelection.SetStartPoint(e.X, e.Y);
                AddToHistory("Создано выделение");
            }
            if (radioButtonPath.Checked)
            {
                paintPath.SetStartPoint(e.X, e.Y, brush, brushSize);
                AddToHistory("Путь");
            }
            if (radioButtonBrushPattern.Checked)
            {
                if (formBrushPattern.paintPattern.points.Count > 0)
                {
                    Random r = new Random();
                    List<Point> list = new List<Point>();
                    List<byte> bytes = formBrushPattern.paintPattern.bytes;
                    foreach (var point in formBrushPattern.paintPattern.points)
                    {
                        if (checkBoxPatternBrushRand.Checked)
                            list.Add(new Point(point.X + e.X - 50 + r.Next(-5, 5), point.Y + e.Y - 50 + r.Next(-5, 5)));
                        else
                            list.Add(new Point(point.X + e.X - 50, point.Y + e.Y - 50));
                    }
                    GraphicsPath graphicsPath = new GraphicsPath(list.ToArray(), bytes.ToArray());
                    graphics.DrawPath(new Pen(brush, brushSize), graphicsPath);
                    AddToHistory("Кисть");
                }
            }
                                


            if (toolDown == false)
            {
                toolDown = true;
            }

           pictureBoxCanvas.Image = bitmap;
        }

        private void pictureBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (toolDown == true)
            {
                if (radioButtonBrushCircle.Checked)
                    graphics.FillEllipse(brush, e.X - brushSize / 2, e.Y - brushSize / 2, brushSize, brushSize);
                if (radioButtonBrushSquare.Checked)
                    graphics.FillRectangle(brush, e.X - brushSize / 2, e.Y - brushSize / 2, brushSize, brushSize);
                if (radioButtonBrushErazer.Checked)
                    graphics.FillRectangle(brushErazer, e.X - brushSize / 2, e.Y - brushSize / 2, brushSize, brushSize);
                if (radioButtonBalon.Checked)
                {
                    if (checkBoxBalonNoise.Checked && checkBoxBalonNoiseCanvas.Checked)
                        paintBalonFilter.FillNoise(e.X, e.Y, brushSize);
                    else
                    {
                        if (checkBoxBalonNoise.Checked)
                            paintBalonFilter.FillNoiseColor(e.X, e.Y, currentColor, brushSize);
                        else
                            paintBalon.Fill(e.X, e.Y, currentColor, brushSize);
                    }
                }
                if (radioButtonBrushXOR.Checked)
                    if (checkBoxBrushXor.Checked)
                        brushXOR.PaintDrawSquareXOR(e.X, e.Y, Color.White, brushSize);
                    else
                        brushXOR.PaintDrawSquareXOR(e.X, e.Y, currentColor, brushSize);
                if (radioButtonBlurBrush.Checked)
                {
                    paintBalonFilter.FillBlur(e.X, e.Y, brushSize);
                }
                if (radioButtonGray.Checked)
                {
                    paintBalonFilter.FillGray(e.X, e.Y, brushSize);
                }
                if (radioButtonPath.Checked)
                {
                    paintPath.AddPathPoint(e.X, e.Y);
                }
                if (radioButtonBrushPattern.Checked)
                {
                    if (formBrushPattern.paintPattern.points.Count > 0)
                    {
                        Random r = new Random();
                        List<Point> list = new List<Point>();
                        List<byte> bytes = formBrushPattern.paintPattern.bytes;
                        foreach (var point in formBrushPattern.paintPattern.points)
                        {
                            if (checkBoxPatternBrushRand.Checked)
                                list.Add(new Point(point.X + e.X - 50 + r.Next(-5, 5), point.Y + e.Y - 50 + r.Next(-5, 5)));
                            else
                                list.Add(new Point(point.X + e.X - 50, point.Y + e.Y - 50));
                        }
                        GraphicsPath graphicsPath = new GraphicsPath(list.ToArray(), bytes.ToArray());
                        graphics.DrawPath(new Pen(brush, brushSize), graphicsPath);
                    }
                }
              

                pictureBoxCanvas.Image = bitmap;
            }

        }

        private void pictureBoxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButtonLine.Checked)
            {
                paintline.SetEndPoint(e.X, e.Y);
                paintline.PaintDrawLine(brush, brushSize);
                AddToHistory("Линия");
            }
            if (radioButtonCircle.Checked)
            {
                paintCircle.SetEndPoint(e.X, e.Y);
                paintCircle.PaintDrawCircle(brush, brushSize);
                AddToHistory("Круг");
            }
            if (radioButtonSquare.Checked)
            {
                paitSquare.SetEndPoint(e.X, e.Y);
                paitSquare.PaintDrawSquare(brush, brushSize);
                AddToHistory("Прямоугольник");
            }
            if (radioButton1.Checked)
            {
                paintRandPoly.SetEndPoint(e.X, e.Y);
                //paintRandPoly.PaintDrawrandPoly(brush, brushSize);
            }
            if (radioButtonGrad.Checked)
            {
                gradient.SetEndPoint(e.X, e.Y);
                if (checkBoxGradXor.Checked)
                {
                    gradient.FillXOR(currentColor, backColor);
                }
                else
                {
                    gradient.Fill(currentColor, backColor);
                }
                AddToHistory("Градиент");
            }
            if (radioButtonWave.Checked)
            {
                paintWave.SetEndPoint(e.X, e.Y);
                try
                {
                    if (checkBox3.Checked)
                        paintWave.PaintDrawWave(Convert.ToDouble(textBoxWaveB.Text), Convert.ToDouble(textBoxWaveV.Text));
                    else
                        paintWave.DrawWave(Convert.ToDouble(textBoxWaveB.Text), Convert.ToDouble(textBoxWaveV.Text));
                    AddToHistory("Волна");
                }
                catch (Exception ee) { }
            }
            if (radioButtonSelectionSquare.Checked)
            {
                paintSelection.SetEndPoint(e.X, e.Y);
                paintSelection.MakeSelection();
            }
            if (radioButtonPath.Checked)
            {
                paintPath.DrawPath();
            }
                  

            if (toolDown == true)
            {
                toolDown = false;
            }
        }

        private void pictureBox1_ColorPicker_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                changeCurrentColorByClick = true;
                currentColor = colorPanelBitmap.GetPixel(e.X, e.Y);
                currentColor = Color.FromArgb(trackBarOpacity.Value, currentColor);
                OnChangeCurrentColor();

                if (changeCurrentColorByClick == true)
                {
                    textBoxColorR.Text = currentColor.R.ToString();
                    textBoxColorG.Text = currentColor.G.ToString();
                    textBoxColorB.Text = currentColor.B.ToString();
                }

                changeCurrentColorByClick = false;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                backColor = colorPanelBitmap.GetPixel(e.X, e.Y);
                backColor = Color.FromArgb(trackBarOpacity.Value, backColor);
                brushErazer.Color = backColor;
                panelBackColor.BackColor = backColor;
            }
        }

        private void trackBarBrushSize_Scroll(object sender, EventArgs e)
        {
            brushSize = trackBarBrushSize.Value;

            Console.WriteLine("Размер кисти: " + trackBarBrushSize.Value);
        }

        private void trackBarColorWidth_Scroll(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
                paintColorPicker.InitColorPanelTest(trackBarColorWidth.Value);

            Console.WriteLine("Количество цветов: " + trackBarColorWidth.Value);
        }

        private void trackBarOpacity_Scroll(object sender, EventArgs e)
        {
            currentColor = Color.FromArgb(trackBarOpacity.Value, currentColor);
            OnChangeCurrentColor();

            Console.WriteLine("Прозрачность цвета: " + trackBarOpacity.Value);
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonClearCanvas_Click(object sender, EventArgs e)
        {
            formPenPattern.Dispose();
            bitmap.Dispose();
            bitmapInit(Convert.ToInt32(textBoxCanvasWidth.Text), Convert.ToInt32(textBoxCanvasHeight.Text));
            Console.WriteLine("Холст очищен/");
        }

        private void bitmapInit(int width, int height)
        {
            
            try
            {
                pictureBoxCanvas.Width = width;
            }
            catch (Exception ee) { }

            try
            {
                pictureBoxCanvas.Height = height;
            }
            catch (Exception ee) { }
            
            bitmap = new Bitmap(pictureBoxCanvas.Width, pictureBoxCanvas.Height);
            bitmap.SetResolution(600.0f, 600.0f);
            graphics = Graphics.FromImage(bitmap);
            baseRegion = graphics.Clip;

            for (int i = 0; i < pictureBoxCanvas.Width; i++)
                for (int j = 0; j < pictureBoxCanvas.Height; j++)
                    bitmap.SetPixel(i, j, backColor);
            //graphics.DrawRectangle(new Pen(Color.FromArgb(254,254,254)), 0, 0, pictureBoxCanvas.Width - 2, pictureBoxCanvas.Height - 2);
            pictureBoxCanvas.Image = bitmap;

            PaintToolsInit();
            try
            {
                AddToHistory("Новое полотно");
            }
            catch (Exception ee) { }
        }

        private void PaintToolsInit()
        {
            pipette = new PaintPipette();
            paintBalon = new PaintBalon(pictureBoxCanvas, bitmap, checkBoxBalonView, graphics);
            paintBalonFilter = new PaintBalonFilter(pictureBoxCanvas, bitmap, checkBoxBalonView, graphics);
            paintline = new PaintLine(pictureBoxCanvas, bitmap, graphics);
            paintCircle = new PaintCircle(pictureBoxCanvas, bitmap, graphics, checkBox2);
            paitSquare = new PaintSquare(pictureBoxCanvas, bitmap, graphics, checkBox2);
            paintRandPoly = new PaintRandomPolygon(pictureBoxCanvas, bitmap, graphics, checkBox2);
            paintText = new PaintText(pictureBoxCanvas, bitmap, graphics);
            paintFill = new PaintFill(pictureBoxCanvas, bitmap);
            paintFillSmooth = new PaintFillSmooth(pictureBoxCanvas, bitmap);
            gradient = new PaintGradient(pictureBoxCanvas, bitmap, graphics);
            brushXOR = new PaintBrushXOR(pictureBoxCanvas, bitmap, graphics);
            paintCloud = new PaintCloud(pictureBoxCanvas, bitmap);
            paintFilterColor = new PaintFilterColor(pictureBoxCanvas, bitmap, progressBarPaintFilter);
            paintFilterNoise = new PaintFilterNoise(pictureBoxCanvas, bitmap, progressBarPaintFilter);
            paintFilterSmooth = new PaintFilterSmooth(pictureBoxCanvas, bitmap, progressBarPaintFilter);
            paintWave = new PaintWave(pictureBoxCanvas, bitmap, graphics);
            paintSelection = new PaintSelection(pictureBoxCanvas, bitmap, graphics);
            paintPath = new PaintPero(pictureBoxCanvas, bitmap, graphics);


            formPenPattern = new FormPenPattern(ref paintPath);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                paintColorPicker.InitColorPanel4();
            }
            else
            {
                paintColorPicker.InitColorPanelTest(trackBarColorWidth.Value);
            }
        }

        private void textBoxColorR_TextChanged(object sender, EventArgs e)
        {
            if (changeCurrentColorByClick == false)
            {
                try
                {
                    int a = Convert.ToInt32(textBoxColorR.Text);
                    if (a >= 0 && a <= 255)
                    {
                        currentColor = Color.FromArgb(trackBarOpacity.Value, Color.FromArgb(a, currentColor.G, currentColor.B));
                        OnChangeCurrentColor();
                    }
                }
                catch (Exception ee) { }
            }
        }

        private void textBoxColorG_TextChanged(object sender, EventArgs e)
        {
            if (changeCurrentColorByClick == false)
            {
                try
                {
                    int a = Convert.ToInt32(textBoxColorR.Text);
                    if (a >= 0 && a <= 255)
                    {
                        currentColor = Color.FromArgb(trackBarOpacity.Value, Color.FromArgb(currentColor.R, a, currentColor.B));
                        OnChangeCurrentColor();
                    }
                }
                catch (Exception ee) { }
            }
        }

        private void textBoxColorB_TextChanged(object sender, EventArgs e)
        {
            if (changeCurrentColorByClick == false)
            {
                try
                {
                    int a = Convert.ToInt32(textBoxColorR.Text);
                    if (a >= 0 && a <= 255)
                    {
                        currentColor = Color.FromArgb(trackBarOpacity.Value, Color.FromArgb(currentColor.R, currentColor.G, a));
                        OnChangeCurrentColor();
                    }
                }
                catch (Exception ee) { }
            }
        }

        private void textBoxColorHex_TextChanged(object sender, EventArgs e)
        {
            if (changeCurrentColorByClick == false)
            {
                try
                {
                    string hexColor = textBoxColorHex.Text;
                    if (hexColor.Length == 6)
                    {
                        string r = hexColor[0].ToString() + hexColor[1].ToString();
                        string g = hexColor[2].ToString() + hexColor[3].ToString();
                        string b = hexColor[4].ToString() + hexColor[5].ToString();

                        int intValueR = Convert.ToInt32(r, 16);
                        int intValueG = Convert.ToInt32(g, 16);
                        int intValueB = Convert.ToInt32(b, 16);

                        if ((intValueR >= 0 && intValueR <= 255) &&
                            (intValueG >= 0 && intValueG <= 255) &&
                            (intValueB >= 0 && intValueB <= 255))
                        {
                            currentColor = Color.FromArgb(trackBarOpacity.Value, Color.FromArgb(intValueR, intValueG, intValueB));
                            OnChangeCurrentColor();
                        }
                    }
                }
                catch (Exception ee) { }
            }
        }

        private void OnChangeCurrentColor()
        {
            brush.Color = currentColor;
            panelCurrentColor.BackColor = currentColor;
            textBoxColorHex.Text = 
                Convert.ToString(currentColor.R, 16).ToUpper() +
                Convert.ToString(currentColor.G, 16).ToUpper() +
                Convert.ToString(currentColor.B, 16).ToUpper();
        }

        private bool saved = false;
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить картинку как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
            {
                try
                {
                    //bitmap.MakeTransparent(Color.White);
                    bitmap.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    saved = true;
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            paintBalon.Strength = Convert.ToInt32(numericUpDown1.Value);
            label7.Text = "Сила балона: " + 100 / paintBalon.Strength + "%";
        }

        private void pictureBoxColorPicker_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toBlackWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterColor.ToBlackWhite();
            AddToHistory("В черно белое");
        }

        private void toRedMinMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterColor.ToRedMax();
        }

        private void toGreenMinMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterColor.ToGreenMax();
        }

        private void toBlueMinMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterColor.ToBlueMax();
        }

        private void toRedGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterColor.ToRedGray();
        }

        private void toGreenGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterColor.ToGreenGray();
        }

        private void toBlueGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterColor.ToBlueGray();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.FilterIndex = 3;
            ofd.Filter = "Jpeg файлы (*.jpg)|*.jpg|Все файлы (*.*)|*.*";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    bitmapInit(Convert.ToInt32(textBoxCanvasWidth.Text), Convert.ToInt32(textBoxCanvasHeight.Text));
                    bitmap = new Bitmap(Image.FromFile(ofd.FileName));
                    graphics = Graphics.FromImage(bitmap);
                    /*
                    if (bitmap.Width >= pictureBoxCanvas.Width || bitmap.Height >= pictureBoxCanvas.Height)
                    {
                        pictureBoxCanvas.SizeMode = PictureBoxSizeMode.Normal;
                    }
                    else
                    {
                        pictureBoxCanvas.SizeMode = PictureBoxSizeMode.AutoSize;
                    }*/
                    pictureBoxCanvas.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBoxCanvas.Image = bitmap;

                    PaintToolsInit();
                    AddToHistory("Открыта картинка");
                }
                catch (Exception ee)
                {
                    Console.WriteLine("открытие изображения с ошибкой");
                }
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
                this.Dispose();
            else
            {
                DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите закрыть без сохранения", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Dispose();
                }
            }
        }
        
        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterSmooth.Smooth3();
            AddToHistory("Смазано");
        }

        private void percentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterNoise.AddNoise(0);
            AddToHistory("Добавлено шума 100%");
        }

        private void percentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            paintFilterNoise.AddNoise(1);
            AddToHistory("Добавлено шума 50%");
        }

        private void percentToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            paintFilterNoise.AddNoise(2);
            AddToHistory("Добавлено шума 30%");
        }

        private void percentToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            paintFilterNoise.AddNoise(10);
            AddToHistory("Добавлено шума 10%");
        }

        private void smootGaussToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterSmooth.SmoothGauss(1);
            AddToHistory("Смазано по Гауссу");
        }

        private void cloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintCloud.DrawLine(250, 250);
        }

        private void buttonAddRed_Click(object sender, EventArgs e)
        {
            paintFilterColor.AddColorChanel(10, "red");
        }

        private void buttonRemoveRed_Click(object sender, EventArgs e)
        {
            paintFilterColor.AddColorChanel(-10, "red");
        }

        private void buttonAddGreen_Click(object sender, EventArgs e)
        {
            paintFilterColor.AddColorChanel(10, "green");
        }

        private void buttonRemoveGreen_Click(object sender, EventArgs e)
        {
            paintFilterColor.AddColorChanel(-10, "green");
        }

        private void buttonAddBlue_Click(object sender, EventArgs e)
        {
            paintFilterColor.AddColorChanel(10, "blue");
        }

        private void buttonRemoveBlue_Click(object sender, EventArgs e)
        {
            paintFilterColor.AddColorChanel(-10, "blue");
        }

        private void smoothGauss2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterSmooth.SmoothGauss2(1);
            AddToHistory("Смазано по Гауссу 2");
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            paintFilterColor.Invert();
            AddToHistory("Инвертирование");
        }

        private void buttonInputImage_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                Image clipboardImage = Clipboard.GetImage();
                bitmapInit(Convert.ToInt32(textBoxCanvasWidth.Text), Convert.ToInt32(textBoxCanvasHeight.Text));
                bitmap = new Bitmap(clipboardImage);
                bitmap.SetResolution(600.0f, 600.0f);
                graphics = Graphics.FromImage(bitmap);
                pictureBoxCanvas.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBoxCanvas.Image = bitmap;
                PaintToolsInit();
                AddToHistory("Вставлено из буфер-обмена");
            }
        }

        private void listBoxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Caretaker caretaker = (Caretaker)listBoxHistory.SelectedItem;
                label14.Text = caretaker.Display;
                originator.SetMemento(caretakers.First(x => x == caretaker).Memento);
                bitmap = (Bitmap) originator.State.Clone();
                graphics = Graphics.FromImage(bitmap);
                PaintToolsInit();
                pictureBoxCanvas.Image = bitmap;

                listBoxHistory.ClearSelected();
            }
            catch (Exception ee) { }
        }

        private void AddToHistory(string action)
        {
            Bitmap bitmap1 = (Bitmap)bitmap.Clone();
            originator.State = bitmap1;
            Caretaker caretaker1 = new Caretaker() { Action = action };
            caretaker1.Number = caretakers.Count;
            caretaker1.Memento = originator.CreateMemento();
            caretakers.Add(caretaker1);
            listBoxHistory.Items.Add(caretaker1);

            saved = false;
        }

        private void buttonClearHistory_Click(object sender, EventArgs e)
        {
            caretakers.RemoveRange(1, caretakers.Count - 1);
            listBoxHistory.Items.Clear();
            listBoxHistory.Items.Add(caretakers[0]);
        }

        private void buttonDenySelection_Click(object sender, EventArgs e)
        {
            paintSelection.RemoveSelection();
            AddToHistory("Выделение снято");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (formPenPattern != null && !formPenPattern.IsDisposed)
                formPenPattern.Show();
            else
            {
                formPenPattern = new FormPenPattern(ref paintPath);
                formPenPattern.Show();
            }
        }

        private void buttonBrushPattern_Click(object sender, EventArgs e)
        {
            if (formBrushPattern != null && !formBrushPattern.IsDisposed)
                formBrushPattern.Show();
            else
            {
                formBrushPattern = new PaintBrushPattern();
                formBrushPattern.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormTest3D form = new FormTest3D();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormTestIzometria form = new FormTestIzometria();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            paintFilterColor.ChangeBrightness(0.1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            paintFilterColor.ChangeBrightness(-0.1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TestPolygon form = new TestPolygon();
            form.Show();
        }

        private void squaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintFilterSmooth.SmoothSquare(15);
            AddToHistory("Фильтр квадраты");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormTestFractal form = new FormTestFractal();
            form.Show();
        }

    }

}
