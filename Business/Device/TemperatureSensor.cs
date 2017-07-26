using System;
using Eaton.Homework.DataModel;

namespace Eaton.Homework.Business
{
    /// <summary>A class which represents a temperature sensor</summary>
    public class TemperatureSensor : Sensor 
    {
        #region CTor
        /// <summary>Creates a new instance of <see cref="TemperatureSensor"/> class.</summary>
        /// <param name="deviceName">A unique name of temperatur sensor</param>
        /// <param name="readingInterval">An interval in milliseconds which defines period between temperature readings.</param>
        public TemperatureSensor(string name, long readingInterval) : base(name, SensorType.Temperature, readingInterval)
        {

        }
        #endregion

        public override void SimulateReading()
        {
            var temperature = new SensorData(DateTime.UtcNow, rand.Next(15, 85).ToString());
            dataBuffer.Add(temperature);
        }
    }
}
