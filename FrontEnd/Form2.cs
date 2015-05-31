using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackEnd;
using System.Threading;

namespace FrontEnd
{
    public partial class Form2 : Form
    {
        private DemoCounter counter;
        private List<int> chromosome;
        private Graphics graphics;
        private Graph graph;
        private int predictAhead;

        public Form2(ref DemoCounter input)
        {
            InitializeComponent();
            counter = input;
        }

        public void SetBestChromosome(List<int> input)
        {
            chromosome = input;
        }

        public void SetPredictAhead(int input)
        {
            predictAhead = input;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            graphics = pictureBox1.CreateGraphics();
            (new Task(() =>
            {
                Thread.Sleep(100);
                List<double> y, y1, y2, y3;
                counter.GetYData(out y, out y1, out y2, out y3);
                graph = new Graph(ref graphics, pictureBox1.Size);
                graph.SetY(y);
                graph.SetPen(new Pen(Color.Black));
                graph.DrawAxes();
                graph.DrawGraph();

                List<double> newY = new List<double>();
                newY.Add(y[0]);
                for (int year = 1; year < y.Count; year++)
                {
                    newY.Add(y[year-1] + counter.CalculateFitForY(chromosome, year - 1, 0) - counter.CalculateFitForY(chromosome, year - 1, 1) + counter.CalculateFitForY(chromosome, year - 1, 2));
                }
                graph.SetY(newY);
                graph.SetPen(new Pen(Color.Blue));
                graph.DrawGraph();
            })).Start();
        }
    }
}
