using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asfalter.Engine.ControlPanel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Control panel for Asfalter Engine Service");
            string command = "";
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                switch (command)
                {
                    case "start":
                        ServiceHelper.Start();
                        break;
                    case "stop":
                        ServiceHelper.Stop();
                        break;
                    case "install":
                        ServiceHelper.Install();
                        break;
                    case "uninstall":
                        ServiceHelper.Uninstall();
                        break;
                }

            } while (command != "exit");
        }
    }
}
