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
            counter.GenerateChromosomesMatrix();
        }
    }
}
