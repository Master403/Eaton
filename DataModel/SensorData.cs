using System;

namespace Eaton.Homework.DataModel
{
    /// <summary>A class which holds generic sensor data</summary>
    [Serializable]
    public class SensorData
    {
        #region Properties
        public DateTime Date;
        public string Value; 
        #endregion

        #region CTor
        public SensorData(DateTime date, string value)
        {
            Date = date;
            Value = value;
        }
        #endregion
    }
}
