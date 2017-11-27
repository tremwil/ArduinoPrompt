using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace ProgramTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Console.Write("Enter baud rate: ");
            int baud = int.Parse(Console.ReadLine());
            SerialPort port = new SerialPort("COM7", baud);
            port.Open();
            port.WriteTimeout = 100000;

            Thread.Sleep(100);

            Stopwatch swatch = new Stopwatch();
            swatch.Start();

            while (true)
            {
                float time = (float)swatch.ElapsedTicks / Stopwatch.Frequency;
                string s1 = $"graph:sine {time} {Math.Sin(time)}\n";
                string s2 = $"graph:sqrt {time} {Math.Sqrt(time)}\n";
                port.Write(s1 + s2);

                Thread.Sleep(2);
            }
        }
    }
}
