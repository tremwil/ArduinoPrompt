using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ArduinoPrompt
{
    public partial class MainWindow
    {
        private Matrix viewTransform = new Matrix();
        private Point prvMousePos;

        private const int maxGridSize = 150;
        private const int minGridSize = 15;
        private const int minTextGridSize = 30;

        private MouseButtons currBtn = MouseButtons.None;

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            //Graph section

            float gridSize = viewTransform.Elements[0];
            double sizeMultiplier = 1;
            while (gridSize > maxGridSize)
            {
                gridSize /= 10;
                sizeMultiplier /= 10;
            }
            while (gridSize < minGridSize)
            {
                gridSize *= 10;
                sizeMultiplier *= 10;
            }

            float w = e.Graphics.ClipBounds.Width;
            float h = e.Graphics.ClipBounds.Height;
            float dx = viewTransform.Elements[4];
            float dy = h - viewTransform.Elements[5];

            Pen axisPen = new Pen(Color.FromArgb(160, Color.LightGray));

            for (float i = dx % gridSize; i < w; i += gridSize)
            {
                Pen p = (i - dx < 1 && i - dx > -1) ? Pens.Black : axisPen;
                e.Graphics.DrawLine(p, i, h, i, 0);

                int s = (int)Math.Round((i - dx) / gridSize);
                if (gridSize >= minTextGridSize || s % 2 == 0)
                {
                    e.Graphics.DrawString(
                        (s * sizeMultiplier).ToString(),
                        SystemFonts.DefaultFont,
                        Brushes.Black,
                        new PointF(i, h - dy)
                    );
                    e.Graphics.DrawLine(Pens.Black, i, h - dy + 4, i, h - dy - 4);
                }
            }

            gridSize = viewTransform.Elements[3];
            sizeMultiplier = 1;
            while (gridSize > maxGridSize)
            {
                gridSize /= 10;
                sizeMultiplier /= 10;
            }
            while (gridSize < minGridSize)
            {
                gridSize *= 10;
                sizeMultiplier *= 10;
            }

            for (float i = dy % gridSize; i < h; i += gridSize)
            {
                Pen p = (i - dy < 1 && i - dy > -1) ? Pens.Black : axisPen;
                e.Graphics.DrawLine(p, 0, h - i, w, h - i);

                int s = (int)Math.Round((i - dy) / gridSize);
                if (gridSize >= minTextGridSize || s % 2 == 0)
                {
                    e.Graphics.DrawString(
                        (s * sizeMultiplier).ToString(),
                        SystemFonts.DefaultFont,
                        Brushes.Black,
                        new PointF(dx, h - i)
                    );
                    e.Graphics.DrawLine(Pens.Black, dx - 4, h - i, dx + 4, h - i);
                }
            }
            axisPen.Dispose();

            // Plot points
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Matrix flipViewTransform = new Matrix(
                viewTransform.Elements[0], 0, 0, -viewTransform.Elements[3],
                viewTransform.Elements[4], viewTransform.Elements[5]
            );

            foreach (object obj in variablePicker.Items)
            {
                Variable var = (Variable)obj;
                if (var.name == "Add new...") { continue; }
                if (var.plotValues.Count < 2) { continue; }

                using (Pen pen = new Pen(var.plotColor, 1))
                {
                    PointF[] screenSpace = var.plotValues.ToArray();
                    flipViewTransform.TransformPoints(screenSpace);
                    e.Graphics.DrawLines(pen, screenSpace);
                }
            }
        }

        private void canvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                float min = Math.Min(viewTransform.Elements[0], viewTransform.Elements[3]);
                viewTransform.Scale(min / viewTransform.Elements[0], min / viewTransform.Elements[3], MatrixOrder.Prepend);

                canvas.Invalidate();
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            currBtn = e.Button;

            if (e.Button == MouseButtons.Left)
            {
                prvMousePos = e.Location;
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            currBtn = MouseButtons.None;
        }


        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                viewTransform.Translate(e.X - prvMousePos.X, e.Y - prvMousePos.Y, MatrixOrder.Append);
                prvMousePos = e.Location;
                canvas.Invalidate();
            }
        }

        private void canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            
            if (viewTransform.Elements[0] > 10e+6 || viewTransform.Elements[3] > 10e+6 && e.Delta > 0)
            {
                return;
            }
            if (viewTransform.Elements[0] < 10e-6 || viewTransform.Elements[3] < 10e-6 && e.Delta < 0)
            {
                return;
            }

            viewTransform.Translate(-e.X, -e.Y, MatrixOrder.Append);

            if (currBtn == MouseButtons.Left || currBtn == MouseButtons.None)
            {
                viewTransform.Scale(1 + (float)e.Delta / 1000, 1, MatrixOrder.Append);
            }
            if (currBtn == MouseButtons.Right || currBtn == MouseButtons.None)
            {
                viewTransform.Scale(1, 1 + (float)e.Delta / 1000, MatrixOrder.Append);
            }

            viewTransform.Translate(e.X, e.Y, MatrixOrder.Append);

            canvas.Invalidate();
        }
    }
}
