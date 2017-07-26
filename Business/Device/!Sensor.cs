using System;
using System.Configuration;
using System.Diagnostics;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Eaton.Homework.DataModel;

namespace Eaton.Homework.Business
{
    public enum SensorType { Unknown = 0, Temperature = 1, Pressure = 2, Vibration = 3 }

    /// <summary>A base class for a sensor which measures different parameters.</summary>
    [DebuggerDisplay("Sensor - {UniqueName}")]
    public abstract class Sensor
    {
        /// <summary>Frequency (in milliseconds) of API call to report data - it could be configured as well.</summary>
        public const int ReportInterval = 10000;

        #region Variables
        private readonly string deviceName;
        private readonly SensorType type;
        private readonly long readingInterval;
        protected SynchronizedCollection<SensorData> dataBuffer;
        /// <summary>A date when devices was switched on.</summary>
        private readonly DateTime startDate;
        private Timer readingTimer;
        private Timer reportTimer;
        protected Random rand;
        #endregion

        #region Properties
        /// <summary>Gets a unique sensor name</summary>
        public string UniqueName => string.Format("{0}_{1}", type.ToString().Substring(0, 4), deviceName);

        #endregion

        #region CTor
        /// <summary>Creates a new instance of <see cref="Device"/> class.</summary>
        /// <param name="deviceName">A unique name of device</param>
        /// <param name="type">A type of sensor</param>
        /// <param name="readingInterval">An interval in milliseconds which defines period between quantity readings.</param>
        public Sensor(string deviceName, SensorType type, long readingInterval)
        {
            if (String.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException(nameof(deviceName));
            }
            startDate = DateTime.UtcNow;
            dataBuffer = new SynchronizedCollection<SensorData>();

            this.deviceName = deviceName;
            this.type = type;
            this.readingInterval = readingInterval;

            // Helper members to simulate a sensor reading
            SetupTimers();
            rand = new Random(startDate.Second);
        }
        #endregion

        #region Public method
        public virtual void SimulateReading()
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region Private methods
        private void SetupTimers()
        {
            readingTimer = new Timer(readingInterval);

            readingTimer.Elapsed += new ElapsedEventHandler(OnReadDataEvent);
            readingTimer.Enabled = true;
            readingTimer.Start();

            reportTimer = new Timer(Sensor.ReportInterval);
            reportTimer.Elapsed += new ElapsedEventHandler(OnReportDataEvent);
            reportTimer.Enabled = true;
            reportTimer.Start();
        }

        private void OnReadDataEvent(object source, ElapsedEventArgs e)
        {
            SimulateReading();
        }

        private void OnReportDataEvent(object source, ElapsedEventArgs e)
        {
            if (dataBuffer.Count > 0)
            {
                List<SensorData> batch = null;
                lock (dataBuffer)
                {
                    batch = dataBuffer.ToList();
                    // I assume that sensor has limited storage for measured data
                    dataBuffer.Clear();
                }
                if (!UploadData(ConfigurationManager.AppSettings["MonitorUrl"], batch))
                {
                    // try to upload again to a backup/second server 
                }
            }
        }

        private bool UploadData(string baseUrl, List<SensorData> data)
        {
            try
            {
                HttpUtility httpUtility = new HttpUtility();
                
                string apiParams = string.Format("deviceName={0}&sensorType={1}", UniqueName, ((int)type).ToString());
                string url = string.Format("{0}/api/device?{1}", baseUrl, apiParams);

                var jsonData = JsonConvert.SerializeObject(data, Formatting.None);
                return httpUtility.PostRequest(url, jsonData);
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("{0} upload failed: {1} ", UniqueName, ex.Message));
                // Either log it or set status of device to "FailedReport" because we miss some data !
                return false;
            }

        }
        #endregion


    }
}
