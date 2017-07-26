using System;
using System.Collections.Generic;
using System.Linq;
using Eaton.Homework.DataModel;

namespace Eaton.Homework.Business
{
    public class SensorManager : ISensorManager
    {
        public const int DeviceNameLenght = 16;

        /// <summary>
        /// Normally we would call DAO layer and read/pass the values from/to the database. 
        /// In order to simplify it let's have a single instance of a data container.
        /// </summary>
        private static SynchronizedCollection<DataStructure> dataContainer = new SynchronizedCollection<DataStructure>();

        public List<string> GetDeviceList()
        {
            return dataContainer
                .GroupBy(x => x.DeviceName)
                .Select(g => g.First())
                .OrderBy(x => x.DeviceName)
                .Select(x => x.DeviceName).ToList();
        }

        public List<SensorData> GetDeviceMessages(string deviceName)
        {
            return dataContainer
                .Where(x => x.DeviceName == deviceName)
                .OrderBy(x => x.ReadingDate)
                .Select(x => new SensorData(x.ReadingDate, x.Value))
                .ToList();
        }

        public List<SensorInfo> GetDeviceStatistics()
        {
            return dataContainer
                .GroupBy(x => x.DeviceName)
                .Select(g => new SensorInfo(g.Key, g.Count()))
                .OrderBy(x => x.DeviceName)
                .ToList();
        }

        public void SaveData(string deviceName, SensorType type, List<SensorData> data)
        {
            foreach (SensorData item in data)
            {
                dataContainer.Add(
                    new DataStructure() { DeviceName = deviceName, ReadingDate = item.Date, Value = item.Value  }
                );
            }
        }
    }

    /// <summary>A helper class to simulate a table</summary>
    public class DataStructure
    {
        public string DeviceName;
        public DateTime ReadingDate;
        public string Value;
    }
}
