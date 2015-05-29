using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoCounter counter = new DemoCounter();
            counter.ReadDataFromFile("data.txt");
            counter.GenerateChromosomesMatrix(1000);
            List<int> output = counter.PerformSearch(100);
            Console.WriteLine(counter.CalculateFitForY(output, 16, 0) + "\t" + counter.CalculateFitForY(output, 16, 1) + "\t" + counter.CalculateFitForY(output, 16, 2));
            Console.ReadKey();
        }
    }
}
