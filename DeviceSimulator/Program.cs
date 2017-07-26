using System;
using System.IO;
using System.Net;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using Eaton.Homework.Business;

namespace DeviceSimulator
{
    class Program
    {
        private static int count = 0;

        static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = 1000;
            Console.WriteLine("-- Device Simulator --");
            CreateSensors();

            Console.WriteLine(" Total number of sensor : {0}", count);

            Console.WriteLine();
            Console.WriteLine("Press any key to abort the simulation");
            Console.ReadKey();
        }

        private static void CreateSensors()
        {
            int temperatureSensorCount = Convert.ToInt32(ConfigurationManager.AppSettings["TemperatureSensorCount"]);
            int pressureSensorCount = Convert.ToInt32(ConfigurationManager.AppSettings["PressureSensorCount"]);
            int vibrationSensorCount = Convert.ToInt32(ConfigurationManager.AppSettings["VibrationSensorCount"]);

            List<Sensor> sensors = new List<Sensor>();
            for (int i = 0; i < temperatureSensorCount; i++)
            {
                sensors.Add(new TemperatureSensor(GenerateUniqueName(), 15000));
            }
            for (int i = 0; i < pressureSensorCount; i++)
            {
                sensors.Add(new PressureSensor(GenerateUniqueName(), 1000));
            }
            for (int i = 0; i < vibrationSensorCount; i++)
            {
                sensors.Add(new VibrationSensor(GenerateUniqueName(), 300));
            }
            count = sensors.Count();
        }

        private static string GenerateUniqueName()
        {
            // Better option would be to use GUID but that's quite a long string to distinguish visually
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}
