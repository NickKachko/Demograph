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

namespace FrontEnd
{
    public partial class Form1 : Form
    {
        private List<List<char>> coefficientMask;
        private const int numberOfFactors = 6, numberOfSpecies = 10000;
        private Form2 form2;
        private DemoCounter counter;

        public void RefreshCheckBox()
        {
            for (int i = 0; i < numberOfFactors; i++)
            {
                checkedListBox1.SetItemChecked(i,coefficientMask[comboBox1.SelectedIndex][i] != 0);
            }
        }

        public Form1()
        {
            InitializeComponent();
            coefficientMask = new List<List<char>>();
            for (int j = 0; j < 3; j++)
            {
                List<char> toInsert = new List<char>();
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add((char)0);
                }
                coefficientMask.Add(toInsert);
            }
            coefficientMask[0][0] = (char)1;
            coefficientMask[0][1] = (char)1;
            coefficientMask[0][3] = (char)1;
            coefficientMask[0][5] = (char)1;
            coefficientMask[1][2] = (char)1;
            coefficientMask[1][4] = (char)1;
            coefficientMask[2][0] = (char)1;
            coefficientMask[2][1] = (char)1;
            counter = new DemoCounter(coefficientMask);
            counter.OnUpdateStatus += new DemoCounter.StatusUpdateHandler(ProgressUpdateHandler);
            try
            {
                counter.ReadDataFromFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }
            comboBox1.SelectedIndex = 0;
            RefreshCheckBox();
        }

        private void ProgressUpdateHandler(double progress)
        {
            progressBar1.Value = (int)(progress * 100);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCheckBox();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                coefficientMask[comboBox1.SelectedIndex][e.Index] = (char)e.NewValue;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form2 = new Form2(ref counter);
            label7.Visible = true;
            counter.GenerateChromosomesMatrix(numberOfSpecies);
            counter.SetMutation((int)numericUpDown4.Value);
            counter.SetError((double)numericUpDown3.Value / 100.0);
            counter.SetYear((int)numericUpDown1.Value);

            form2.SetBestChromosome(counter.PerformSearch((int)numericUpDown2.Value));
            form2.SetPredictAhead((int)numericUpDown1.Value);
            progressBar1.Value = 100;
            label7.Visible = false;
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.CheckFileExists)
            {
                try
                {
                    counter.ReadDataFromFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured: " + ex.Message);
                }
            }
        }
    }
}
