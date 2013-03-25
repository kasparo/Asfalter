using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Asfalter.Engine
{
    internal static class Logger
    {
        public static void Write(string text)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logger.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine(text);
                writer.WriteLine("-----------------------------");
            }
        }
    }
}
