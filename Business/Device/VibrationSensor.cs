using System;
using Eaton.Homework.DataModel;

namespace Eaton.Homework.Business
{
    /// <summary>A class which represents a vibration sensor</summary>
    public class VibrationSensor : Sensor
    {
        #region CTor
        /// <summary>
        /// Creates a new instance of <see cref="VibrationSensor"/> class.</summary>
        /// <param name="name">A unique name of vibration sensor</param>
        /// <param name="readingInterval">An interval in milliseconds which defines period between vibration level readings.</param>
        public VibrationSensor(string name, long readingInterval) : base(name, SensorType.Vibration, readingInterval)
        {

        }
        #endregion

        public override void SimulateReading()
        {
            // simulate multiple values 
            var vibrationValues = string.Format("{0}|{1}", rand.Next(0, 10000).ToString(), rand.Next(0, 10));
            dataBuffer.Add(new SensorData(DateTime.UtcNow, vibrationValues));
        }
    }
}
