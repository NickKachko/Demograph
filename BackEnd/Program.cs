using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    class Program
    {
        static List<double> y, y1, y2, y3;
        const int forYear = 17;
        static void Main(string[] args)
        {
            DemoCounter counter = new DemoCounter();
            counter.ReadDataFromFile("data.txt");
            counter.GenerateChromosomesMatrix(10);
            List<int> output = counter.PerformSearch(10);
            counter.GetYData(out y, out y1, out y2, out y3);
            Console.WriteLine(y1[forYear] + "\t" + y2[forYear] + "\t" + y3[forYear]);
            Console.WriteLine(counter.CalculateFitForY(output, forYear - 1, 0) + "\t" + counter.CalculateFitForY(output, forYear - 1, 1) + "\t" + counter.CalculateFitForY(output, forYear-1, 2));
            Console.ReadKey();
        }
    }
}
