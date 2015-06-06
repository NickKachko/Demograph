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
        private const int startingYear = 1996;

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
                List<double> y, y1, y2, y3, yNew;
                double a1, a2, a3;
                counter.GetYData(out y, out y1, out y2, out y3);
                graph = new Graph(ref graphics, pictureBox1.Size);
                yNew = new List<double>();
                graph.SetY(y);
                graph.SetNewY(yNew);
                graph.SetPen(new Pen(Color.Black));
                graph.SetPredict(predictAhead);
                int to = y.Count + predictAhead;

                for (int year = predictAhead; year < y.Count; year++)
                {
                    a1=counter.CalculateFitForY(chromosome, year - predictAhead, 0) * 0.2 + y1[year] * 0.8;
                    a2=counter.CalculateFitForY(chromosome, year - predictAhead, 1) * 0.2 + y2[year] * 0.8;
                    a3=counter.CalculateFitForY(chromosome, year - predictAhead, 2) * 0.2 + y3[year] * 0.8;
                    yNew.Add(y[year] + a1 - a2 + a3);
                }

                for (int year = y.Count; year < to; year++)
                {
                    y1.Add(counter.CalculateFitForY(chromosome, year - predictAhead, 0) * 0.2 + y1[y1.Count - 1] * 0.8);
                    y2.Add(counter.CalculateFitForY(chromosome, year - predictAhead, 1) * 0.2 + y2[y2.Count - 1] * 0.8);
                    y3.Add(counter.CalculateFitForY(chromosome, year - predictAhead, 2) * 0.2 + y3[y3.Count - 1] * 0.8);
                    double result = yNew[yNew.Count - 1] + y1[y1.Count - 1] - y2[y2.Count - 1] + y3[y3.Count - 1];
                    //y.Add(result);
                    yNew.Add(result);
                }


                graph.DrawAxes();
                graph.DrawGraph();

                for (int year = yNew.Count; year < to; year++)
                {
                    double result = y[y.Count - 1] + y1[year] - y2[year] + y3[year];
                    y.Add(result);
                }

                //List<double> newY = new List<double>();
                //newY.Add(y[0]);
                //for (int year = predictAhead; year <= y.Count - 1; year++)
                //{
                //    newY.Add(y[year - 1] + counter.CalculateFitForY(chromosome, year - predictAhead, 0) - counter.CalculateFitForY(chromosome, year - predictAhead, 1) + counter.CalculateFitForY(chromosome, year - predictAhead, 2));
                //}
                //for (int year = y.Count; year < y.Count + predictAhead; year++)
                //{
                //    newY.Add(newY[newY.Count - 1] + counter.CalculateFitForY(chromosome, year - predictAhead, 0) - counter.CalculateFitForY(chromosome, year - predictAhead, 1) + counter.CalculateFitForY(chromosome, year - predictAhead, 2));
                //}

                //graph.SetY(newY);
                //graph.SetPen(new Pen(Color.Blue));
                //graph.DrawGraph();
                
                //DataGridViewRow row = new DataGridViewRow();
                //row.
                for (int i = 0; i<y.Count; i++)
                {
                    dataGridView1.Rows.Add(startingYear + i, y1[i], y2[i], y3[i], y[i]);
                    if (i > 17)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Blue };
                    }
                }
                int al, bl=5;
                al = bl;
                //for (int i = 0; i < predictAhead; i++)
                //{
                //    dataGridView1.Rows.Add(startingYear + y.Count + i, counter.CalculateFitForY(chromosome, y.Count - predictAhead + i, 0), counter.CalculateFitForY(chromosome, y.Count - predictAhead + i, 1), counter.CalculateFitForY(chromosome, y.Count - predictAhead + i, 2), newY[newY.Count - predictAhead + i]);
                //}
            })).Start();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            graphics = pictureBox1.CreateGraphics();
            (new Task(() =>
            {
                Thread.Sleep(100);
                graph.SetGraphics(ref graphics);
                graph.SetPen(new Pen(Color.Black));
                graph.DrawAxes();
                graph.DrawGraph();
            })).Start();
        }
    }
}
