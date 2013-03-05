using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Asfalter.UnitSimulator
{
    class Program
    {
        private static List<Unit> Units { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Unit Simulator");
            Console.WriteLine("Set unit count ...");
            Console.Write(">");

            int unitCount = int.Parse(Console.ReadLine());
            Units = new List<Unit>(unitCount);

            for (int i = 1; i <= unitCount; i++)
            {
                Thread thread = new Thread(new ThreadStart(Go));
                thread.Name = "UnitThread";
                thread.Start();
            }

            string command = "";
            do
            {
                Console.Write(">");
                command = Console.ReadLine();
            } while (command != "stop");

            foreach (Unit unit in Units)
                unit.Stop();
        }

        private static object locker = new object();
        static void Go()
        {
            Unit unitOne = new Unit();

            lock (locker)
            {
                Units.Add(unitOne);
            }
            unitOne.Start();
        }
    }
}
