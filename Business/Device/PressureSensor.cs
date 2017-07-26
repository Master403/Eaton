using System;
using Eaton.Homework.DataModel;

namespace Eaton.Homework.Business
{
    /// <summary>A class which represents a pressure sensor</summary>
    public class PressureSensor : Sensor
    {
        #region CTor
        /// <summary>Creates a new instance of <see cref="PressureSensor"/> class.</summary>
        /// <param name="name"></param>
        /// <param name="readingInterval"></param>
        public PressureSensor(string name, long readingInterval) : base(name, SensorType.Pressure, readingInterval)
        {

        }
        #endregion

        public override void SimulateReading()
        {
            var pressure = Math.Round(1 + rand.NextDouble(), 4);
            dataBuffer.Add(new SensorData(DateTime.UtcNow, pressure.ToString()));
        }
    }
}
