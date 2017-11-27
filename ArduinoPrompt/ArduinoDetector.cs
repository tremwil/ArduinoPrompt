using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace ArduinoPrompt
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
}
