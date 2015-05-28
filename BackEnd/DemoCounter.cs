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

        private double CalculateFitForY(List<int> input,int year, int numberY)
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

        private void PerformCrossover()
        {

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

        public void GenerateChromosomesMatrix()
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

        public void PerformSearch(int numberOfGenerations = 1)
        {
            List<double> a1 = new List<double>();
            List<double> a2 = new List<double>();

            var sorted = matrixOfChromosomes.OrderBy(g => CalculateFitness(g)).ToList();
            matrixOfChromosomes.ForEach(h => a1.Add(CalculateFitness(h)));
            sorted.ForEach(h => a2.Add(CalculateFitness(h)));
        }
    }
}
