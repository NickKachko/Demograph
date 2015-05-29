/*
 * A class for main logic and algorithms implementation 
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    class DemoCounter
    {
        private List<double> f1, f2, f3;
        private const int numberOfFactors = 6, numberOfCoefficients = 9, predictAhead  = 1;
        private List<List<int>> matrixOfChromosomes;
        private List<List<char>> coefficientMask;
        private List<List<double>> x;
        private Random rand;

        public double CalculateFitForY(List<int> input,int year, int numberY)
        {
            double answer = 0;
            for (int i = 0; i < numberOfFactors; i++)
            {
                answer += (input[i] * x[year][i]) * coefficientMask[numberY][i];
                answer += (input[numberOfFactors + i] * Math.Pow(x[year][i], 2)) * coefficientMask[numberY][i];
                answer += (input[2 * numberOfFactors + i] * Math.Pow(Math.E, input[3 * numberOfFactors + i] * x[year][i])) * coefficientMask[numberY][i];
                answer += (input[4 * numberOfFactors + i] * Math.Pow(x[year][i], input[5 * numberOfFactors + i])) * coefficientMask[numberY][i];
                answer += (input[6 * numberOfFactors + i] / x[year][i]) * coefficientMask[numberY][i];
                if (Math.Abs(input[8 * numberOfFactors + i]) >= 1)
                    answer += (input[7 * numberOfFactors + i] * Math.Log(input[8 * numberOfFactors + i] * x[year][i], Math.E)) * coefficientMask[numberY][i];
            }
            return answer;
        }

        private double CalculateFitness(List<int> input)
        {
            double localY1 = 0, localY2 = 0, localY3 = 0;
            for (int year = 0; year < x.Count - predictAhead; year++)
            {
                localY1 += Math.Abs(CalculateFitForY(input, year, 0) - f1[year + predictAhead]) / f1[year + predictAhead];
                localY2 += Math.Abs(CalculateFitForY(input, year, 1) - f2[year + predictAhead]) / f2[year + predictAhead];
                localY3 += Math.Abs(CalculateFitForY(input, year, 2) - f3[year + predictAhead]) / f3[year + predictAhead];

            }
            return double.IsNaN(localY1 + localY2 + localY3)  ? double.MaxValue : (localY1 + localY2 + localY3);
        }

        private List<List<int>> PerformIteration()
        {
            List<List<int>> output = new List<List<int>>();
            output.Add(matrixOfChromosomes[0]);

            for (int i = 1; i < matrixOfChromosomes.Count; i++ )
            {
                output.Add(Crossover(matrixOfChromosomes[0], matrixOfChromosomes[i]));
            }

            return output;
        }

        private List<int> Crossover(List<int> a1, List<int> b1)
        {
            List<int> a = new List<int>(a1);
            List<int> b = new List<int>(b1);
            List<int> temp;
            int left, middle = 0, right;
            left = rand.Next(a.Count / 2);
            right = rand.Next(a.Count / 2) + a.Count / 2;
            middle = rand.Next(right - left - 1) + left + 1;

            temp = a.GetRange(left, middle - left);
            a.RemoveRange(left, middle - left);
            b.InsertRange(left, temp);
            temp = b.GetRange(middle, middle - left);
            b.RemoveRange(middle, middle - left);
            a.InsertRange(left, temp);

            temp = a.GetRange(middle, right - middle);
            a.RemoveRange(middle, right - middle);
            b.InsertRange(middle, temp);
            temp = b.GetRange(right, right - middle);
            b.RemoveRange(right, right - middle);
            a.InsertRange(middle, temp);

            return CalculateFitness(a) < CalculateFitness(b) ? a : b;
        }

        public DemoCounter()
        {
            f1 = new List<double>();
            f2 = new List<double>();
            f3 = new List<double>();
            matrixOfChromosomes = new List<List<int>>();
            x = new List<List<double>>();
            rand = new Random();
            coefficientMask = new List<List<char>>();
            for (int j = 0; j<3; j++)
            {
                List<char> toInsert = new List<char>();
                for (int i = 0; i<numberOfFactors; i++)
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
        }

        public void ReadDataFromFile(String input)
        {
            StreamReader streamReader = new StreamReader(input);
            string row = streamReader.ReadLine();
            while (row != null)
            {
                var entries = row.Split('\t');
                f1.Add(double.Parse(entries[1]));
                f2.Add(double.Parse(entries[2]));
                f3.Add(double.Parse(entries[3]));
                x.Add(new List<double>());
                for (int i = 5; i<11; i++)
                {
                    x[x.Count-1].Add(double.Parse(entries[i]));
                }
                row = streamReader.ReadLine();
            }
        }

        public void GenerateChromosomesMatrix(int factor)
        {
            for (int k = 0; k < factor; k++)
            {
                List<int> toInsert = new List<int>();
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                for (int i = 0; i < numberOfFactors * (numberOfCoefficients - 1); i++)
                {
                    toInsert.Add(0);
                }
                matrixOfChromosomes.Add(toInsert);


                toInsert = new List<int>();
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(0);
                }
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                for (int i = 0; i < numberOfFactors * (numberOfCoefficients - 2); i++)
                {
                    toInsert.Add(0);
                }
                matrixOfChromosomes.Add(toInsert);


                toInsert = new List<int>();
                for (int i = 0; i < numberOfFactors * 2; i++)
                {
                    toInsert.Add(0);
                }
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                for (int i = 0; i < numberOfFactors * (numberOfCoefficients - 4); i++)
                {
                    toInsert.Add(0);
                }
                matrixOfChromosomes.Add(toInsert);


                toInsert = new List<int>();
                for (int i = 0; i < numberOfFactors * 4; i++)
                {
                    toInsert.Add(0);
                }
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                for (int i = 0; i < numberOfFactors * (numberOfCoefficients - 6); i++)
                {
                    toInsert.Add(0);
                }
                matrixOfChromosomes.Add(toInsert);


                toInsert = new List<int>();
                for (int i = 0; i < numberOfFactors * 6; i++)
                {
                    toInsert.Add(0);
                }
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                for (int i = 0; i < numberOfFactors * (numberOfCoefficients - 7); i++)
                {
                    toInsert.Add(0);
                }
                matrixOfChromosomes.Add(toInsert);


                toInsert = new List<int>();
                for (int i = 0; i < numberOfFactors * 7; i++)
                {
                    toInsert.Add(0);
                }
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                for (int i = 0; i < numberOfFactors; i++)
                {
                    toInsert.Add(rand.Next(21) - 10);
                }
                matrixOfChromosomes.Add(toInsert);
            }
        }

        public List<int> PerformSearch(int numberOfGenerations = 1)
        {
            DateTime start = DateTime.Now;
            for (int i = 0; i < numberOfGenerations; i++)
            {
                matrixOfChromosomes = matrixOfChromosomes.OrderBy(g => CalculateFitness(g)).ToList();
                matrixOfChromosomes = PerformIteration();
                DateTime now = DateTime.Now;
                if (start.AddMilliseconds(500)<now)
                {
                    start = now;
                    Console.Clear();
                    Console.WriteLine(i / (double)numberOfGenerations);
                }
            }
            matrixOfChromosomes = matrixOfChromosomes.OrderBy(g => CalculateFitness(g)).ToList();
            return matrixOfChromosomes[0];
        }
    }
}
