namespace Eaton.Homework.DataModel
{
    /// <summary>A class which holds info about a sensor</summary>
    public class SensorInfo
    {
        public string DeviceName;
        public int MessageCount;

        public SensorInfo(string deviceName, int count)
        {
            DeviceName = deviceName;
            MessageCount = count;
        }
    }
}
