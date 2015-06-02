using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd
{
    public class Graph
    {
        private Graphics graphics;
        private Pen axisPen, graphPen;
        private Font font;
        private Size size;
        private Point zeroAxis, xAxis, yAxis;
        private double xStart = 1996, yStart = 0, xEnd, yEnd = 100000;
        private List<double> y, y1, y2, y3;

        private void FindMinMax()
        {
            xEnd = xStart + y.Count;
            for (int i = 0; i<y.Count; i++)
            {
                if (y[i]>yStart)
                {
                    yStart = y[i];
                }
                if (y[i]<yEnd)
                {
                    yEnd = y[i];
                }
            }
        }

        public Graph(ref Graphics input, Size size)
        {
            zeroAxis = new Point(40, size.Height - 20);
            xAxis = new Point(size.Width - 20, size.Height - 20);
            yAxis = new Point(40, 20);
            axisPen = new Pen(Color.Black);
            font = new Font(FontFamily.GenericSerif, 10);
            graphics = input;
            this.size = size;
        }

        public void SetY(List<double> y)
        {
            this.y = y;
        }

        public void SetPen(Pen toSet)
        {
            this.graphPen = toSet;
        }

        public void DrawAxes()
        {
            FindMinMax();
            graphics.DrawLine(axisPen, zeroAxis, xAxis);
            graphics.DrawLine(axisPen, zeroAxis, yAxis);
            graphics.DrawString(((int)xStart).ToString(), font, axisPen.Brush, new PointF(zeroAxis.X, zeroAxis.Y + 5));
            graphics.DrawString(((int)yEnd).ToString(), font, axisPen.Brush, new PointF(zeroAxis.X - 40, zeroAxis.Y - 10));
            graphics.DrawString(((int)xEnd).ToString(), font, axisPen.Brush, new PointF(xAxis.X - 10, xAxis.Y + 5));
            graphics.DrawString(((int)yStart).ToString(), font, axisPen.Brush, new PointF(yAxis.X - 40, yAxis.Y - 0));
        }

        public void DrawGraph()
        {
            for (int i = 0; i<y.Count - 1; i++)
            {
                if (i>17)
                {
                    graphPen = new Pen(Color.Blue);
                }
                Point a, b;
                a = new Point((int)(zeroAxis.X + i * (xAxis.X - zeroAxis.X) / (double) (y.Count)), (int)(zeroAxis.Y - (zeroAxis.Y - yAxis.Y) * (y[i] - yEnd) / (double) (yStart - yEnd)));
                b = new Point((int)(zeroAxis.X + (i + 1) * (xAxis.X - zeroAxis.X) / (double)(y.Count)), (int)(zeroAxis.Y - (zeroAxis.Y - yAxis.Y) * (y[i + 1] - yEnd) / (double)(yStart - yEnd)));
                graphics.DrawLine(graphPen, a, b);
                graphics.DrawLine(axisPen, new Point(b.X, zeroAxis.Y - 4), new Point(b.X, zeroAxis.Y + 4));
                graphics.DrawLine(graphPen, new Point(zeroAxis.X - 4, a.Y), new Point(zeroAxis.X + 4, a.Y));
            }
        }
    }
}
