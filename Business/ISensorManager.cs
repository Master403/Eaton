using System.Collections.Generic;
using Eaton.Homework.DataModel;

namespace Eaton.Homework.Business
{
    /// <summary>Defines methods for working with sensors</summary>
    public interface ISensorManager
    {
        /// <summary>Returns a list of active devices</summary>
        /// <returns>A list of device names</returns>
        List<string> GetDeviceList();

        /// <summary>Gets a list of all messages for given device</summary>
        /// <param name="deviceName">A device name</param>
        /// <returns>A list of sensor data</returns>
        List<SensorData> GetDeviceMessages(string deviceName);

        /// <summary>Returns a list of active devices and number of their messages</summary>
        /// <returns>A list of <see cref="SensorInfo"/> classes</returns>
        List<SensorInfo> GetDeviceStatistics();

        /// <summary>Stores device data to the database</summary>
        /// <param name="deviceName">A device name</param>
        /// <param name="type">A sensor type</param>
        /// <param name="data">A collection of sensor's data</param>
        void SaveData(string deviceName, SensorType type, List<SensorData> data);
    }
}
