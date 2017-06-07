using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TrainingData TD = new TrainingData();
            Console.WriteLine(TD.GATEtest(TrainingData.Gates.AND,100000, 3, 0.5f, 0.1f));
            Console.WriteLine(TD.GATEtest(TrainingData.Gates.OR, 100000, 3, 0.5f, 0.1f));
            Console.WriteLine(TD.GATEtest(TrainingData.Gates.XOR, 100000, 3, 0.5f, 0.1f));
            Console.WriteLine(TD.BINARYtest(100000, 3, 0.5f, 0.1f));
            Console.ReadLine();
        }
    }
}
