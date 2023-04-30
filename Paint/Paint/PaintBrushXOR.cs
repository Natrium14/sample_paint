using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintBrushXOR : PaintBase
    {
        public PaintBrushXOR(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {
            this.bitmap = bitmap;
            this.pictureBox = pictureBox;
            this.graphics = graphics;
        }

        public void PaintDrawSquareXOR(int x, int y, Color fillColor, int brushSize)
        {
            int xSize = x - brushSize / 2;
            int ySize = y - brushSize / 2;
            Color curColor;
            var selectionBounds = graphics.VisibleClipBounds;

            try // вместо трай написать условия проверки на край холста
            {
                for (int i = xSize; i < (xSize + brushSize); i++)
                    for (int j = ySize; j < (ySize + brushSize); j++)
                    {
                        if (selectionBounds.Contains(new Rectangle(i, j, 1, 1)))
                        {
                            curColor = bitmap.GetPixel(i, j);
                            int r = curColor.R ^ fillColor.R;
                            int g = curColor.G ^ fillColor.G;
                            int b = curColor.B ^ fillColor.B;
                            bitmap.SetPixel(i, j, Color.FromArgb(r, g, b));
                        }
                    }

                pictureBox.Image = bitmap;
            }
            catch (Exception ee) { }
        }
    }
}
