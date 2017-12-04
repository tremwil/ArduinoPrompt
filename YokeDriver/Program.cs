using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Management;
using System.IO.Ports;
using System.Diagnostics;

namespace YokeDriver
{
    public static class ArduinoDetector
    {
        public static List<string> GetArduinoPorts()
        {
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            List<string> arduinoPorts = new List<string>();

            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    string desc = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();

                    if (desc.Contains("Arduino"))
                    {
                        arduinoPorts.Add(deviceId);
                    }
                }
            }
            catch (ManagementException e)
            {
                /* Do Nothing */
            }

            return arduinoPorts;
        }
    }

    public class Program
    {
        static SerialPort port;
        static byte[] unprocessed;

        static Stopwatch timer;
        static long lastTime = -1, maxDt = 0;

        static float roll, pitch, throttle;

        public static void Main()
        {
            List<string> ports;
            unprocessed = new byte[0];

            do
            {
                ports = ArduinoDetector.GetArduinoPorts();
                Thread.Sleep(10);
            } while (ports.Count == 0);

            port = new SerialPort(ports[0], 115200);
            port.Open();
            port.ReadExisting();

            Console.WriteLine($"Connected on Arduino port {ports[0]}");

            // Send magic number
            Thread.Sleep(2500);
            port.Write(new byte[1] { 0x69 }, 0, 1);

            timer = new Stopwatch();
            timer.Start();

            Thread consoleThread = new Thread(ConsoleThread);
            consoleThread.Start();

            while (true)
            {
                int toRead = port.BytesToRead;
                byte[] buffer = new byte[unprocessed.Length + toRead];
                port.Read(buffer, unprocessed.Length, toRead);
                unprocessed.CopyTo(buffer, 0);

                float[] vals = new float[3];

                int i;
                for (i = 0; i + 11 < buffer.Length; i += 12)
                {
                    for (int j = 0; j < 12; j += 4)
                    {
                        vals[j >> 2] = Math.Min(Math.Max(BitConverter.ToSingle(buffer, i + j), -1), 1);
                    }
                }
                unprocessed = buffer.Skip(i).ToArray();

                if (i != 0)
                {
                    roll = vals[0];
                    pitch = vals[2];
                    throttle = vals[1];

                    long ctime = timer.ElapsedMilliseconds;
                    long dt = ctime - ((lastTime < 0) ? ctime : lastTime);
                    lastTime = ctime;

                    if (maxDt < dt) { maxDt = dt; }
                }
            }
        }

        static void ConsoleThread()
        {
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"ROLL      : {roll}".PadRight(Console.BufferWidth - 1));
                Console.WriteLine($"PITCH     : {pitch}".PadRight(Console.BufferWidth - 1));
                Console.WriteLine($"THROTTLE  : {throttle}".PadRight(Console.BufferWidth - 1));
                Console.WriteLine($"MAX DTIME : {maxDt}".PadRight(Console.BufferWidth - 1));
                Console.WriteLine($"UNP LEN   : {unprocessed.Length}".PadRight(Console.BufferWidth - 1));

                Thread.Sleep(50);
            }
        }
     }
}
