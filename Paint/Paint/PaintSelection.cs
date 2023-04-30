using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    public class PaintSelection: PaintLine
    {
        public PaintSelection(PictureBox pictureBox, Bitmap bitmap, Graphics graphics)
            : base(pictureBox, bitmap, graphics)
        {
            bounds = new List<SelectionBoundPoint>();
        }

        private List<SelectionBoundPoint> bounds;

        private struct SelectionBoundPoint
        {
            public Point BoundPoint { get; set; }
            public Color BoundPointColor { get; set; }
        }        

        public void MakeSelection()
        {
            RemoveSelection();
            Rectangle r = new Rectangle(startPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));

            if (endPoint.X > pictureBox.Width - 2)
            {
                endPoint.X = pictureBox.Width - 2;
            }
            if (endPoint.X <= 0)
            {
                endPoint.X = 1;
            }
            if (endPoint.Y > pictureBox.Height - 2)
            {
                endPoint.Y = pictureBox.Height - 2;
            }
            if (endPoint.Y <= 0)
            {
                endPoint.Y = 1;
            }

            if ((endPoint.X > startPoint.X) && (endPoint.Y > startPoint.Y))
            {
                r = new Rectangle(startPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));

                bounds = new List<SelectionBoundPoint>();
                for (int i = startPoint.X - 1; i <= endPoint.X + 1; i++)
                {
                    for (int j = startPoint.Y - 1; j <= endPoint.Y + 1; j++)
                    {
                        if (j == startPoint.Y - 1) // верх
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (j == endPoint.Y + 1) // низ
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (i == startPoint.X - 1) // лево
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (i == endPoint.X + 1) // право
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                    }
                }

                bounds = bounds.Distinct().ToList();

                foreach (var p in bounds)
                {
                    bitmap.SetPixel(p.BoundPoint.X, p.BoundPoint.Y, Color.Black);
                }

                pictureBox.Image = bitmap;
            }
            if ((endPoint.X < startPoint.X) && (endPoint.Y < startPoint.Y))
            {
                RemoveSelection();
                r = new Rectangle(endPoint.X, endPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));

                bounds = new List<SelectionBoundPoint>();
                for (int i = endPoint.X - 1; i <= startPoint.X - 1; i++)
                {
                    for (int j = endPoint.Y - 1; j <= startPoint.Y - 1; j++)
                    {
                        if (j == startPoint.Y - 1) // низ
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (j == endPoint.Y - 1) // верх
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (i == startPoint.X - 1) // право
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (i == endPoint.X - 1) // лево
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                    }
                }

                bounds = bounds.Distinct().ToList();

                foreach (var p in bounds)
                {
                    bitmap.SetPixel(p.BoundPoint.X, p.BoundPoint.Y, Color.Black);
                }

                pictureBox.Image = bitmap;
            }
            if ((endPoint.X > startPoint.X) && (endPoint.Y < startPoint.Y))
            {
                RemoveSelection();
                r = new Rectangle(startPoint.X, endPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));

                bounds = new List<SelectionBoundPoint>();
                for (int i = startPoint.X - 1; i <= endPoint.X - 1; i++)
                {
                    for (int j = endPoint.Y - 1; j <= startPoint.Y - 1; j++)
                    {
                        if (j == startPoint.Y - 1) // низ
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (j == endPoint.Y - 1) // верх
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (i == startPoint.X - 1) // лево
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (i == endPoint.X - 1) // право
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                    }
                }

                bounds = bounds.Distinct().ToList();

                foreach (var p in bounds)
                {
                    bitmap.SetPixel(p.BoundPoint.X, p.BoundPoint.Y, Color.Black);
                }

                pictureBox.Image = bitmap;
            }
            if ((endPoint.X < startPoint.X) && (endPoint.Y > startPoint.Y))
            {
                RemoveSelection();
                r = new Rectangle(endPoint.X, startPoint.Y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));

                bounds = new List<SelectionBoundPoint>();
                for (int i = endPoint.X - 1; i <= startPoint.X - 1; i++)
                {
                    for (int j = startPoint.Y - 1; j <= endPoint.Y - 1; j++)
                    {
                        if (j == startPoint.Y - 1) // верх
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (j == endPoint.Y - 1) // низ
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (i == startPoint.X - 1) // право
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                        if (i == endPoint.X - 1) // лево
                        {
                            bounds.Add(new SelectionBoundPoint()
                            {
                                BoundPoint = new Point(i, j),
                                BoundPointColor = bitmap.GetPixel(i, j)
                            });
                        }
                    }
                }

                bounds = bounds.Distinct().ToList();

                foreach (var p in bounds)
                {
                    bitmap.SetPixel(p.BoundPoint.X, p.BoundPoint.Y, Color.Black);
                }

                pictureBox.Image = bitmap;
            }

            Region reg = new Region(r);
            //Region reg1 = new Region(path?);

            graphics.Clip = reg;
        }

        public void RemoveSelection()
        {
            foreach (var p in bounds)
            {
                bitmap.SetPixel(p.BoundPoint.X, p.BoundPoint.Y, Color.FromArgb(p.BoundPointColor.R, p.BoundPointColor.G, p.BoundPointColor.B));
            }

            graphics.ResetClip();
            pictureBox.Image = bitmap;
        }
    }
}
