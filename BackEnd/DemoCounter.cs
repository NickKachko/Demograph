/*
 * A class for main logic and algorithms implementation 
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    class DemoCounter
    {
        private List<double> f1, f2, f3, x1, x2, x3, x4, x5, x6;
        private const int numberOfFactors = 6, numberOfCoefficients = 9;
        private List<List<int>> matrixOfChromosomes;
        private Random rand;

        public DemoCounter()
        {
            f1 = new List<double>();
            f2 = new List<double>();
            f3 = new List<double>();
            x1 = new List<double>();
            x2 = new List<double>();
            x3 = new List<double>();
            x4 = new List<double>();
            x5 = new List<double>();
            x6 = new List<double>();
            matrixOfChromosomes = new List<List<int>>();
            rand = new Random();
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
                x1.Add(double.Parse(entries[5]));
                x2.Add(double.Parse(entries[6]));
                x3.Add(double.Parse(entries[7]));
                x4.Add(double.Parse(entries[8]));
                x5.Add(double.Parse(entries[9]));
                x6.Add(double.Parse(entries[10]));
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
    }
}
