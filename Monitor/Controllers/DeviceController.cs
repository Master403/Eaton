using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using Eaton.Homework.DataModel;
using Eaton.Homework.Business;
using Eaton.Homework.Monitor.Models;

namespace Eaton.Homework.Controllers
{
    public class DeviceController : ApiController
    {
        /// <summary>Contains business logic related to <see cref="Sensor"/> objects.</summary>
        private readonly ISensorManager sensorManagerBll;
        /// <summary>Contains methods to log errors</summary>
        private readonly ILogManager logManager;

        /// <summary>Initializes a new instance of the <see cref="DeviceController"/> class.</summary>
        /// <param name="sensorManagerBll">Contains business logic related to <see cref="Sensor"/> objects.</param>
        /// <param name="logManager">Contains method to log erros</param>
        public DeviceController(ISensorManager sensorManagerBll, ILogManager logManager)
        {
            if (sensorManagerBll == null) throw new ArgumentNullException(nameof(sensorManagerBll));
            if (logManager == null) throw new ArgumentNullException(nameof(logManager));

            this.sensorManagerBll = sensorManagerBll;
            this.logManager = logManager;
        }

        [HttpGet]
        public IHttpActionResult DeviceInfo(string deviceName = null)
        {
            try
            {
                if (string.IsNullOrEmpty(deviceName))
                {
                    return Ok(sensorManagerBll.GetDeviceList());
                }
                else
                {
                    var data = sensorManagerBll.GetDeviceMessages(deviceName)
                        .Select(x => new SensorDataModel { RecordingDate = x.Date, RecordingValue = x.Value }).ToList();
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                logManager.Log("Failed to load devices", ex, LogLevel.Error);
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult ReportData(string deviceName, int sensorType, List<SensorData> sensorData)
        {
            try
            {
                if (string.IsNullOrEmpty(deviceName) || deviceName.Length != SensorManager.DeviceNameLenght)
                {
                    // optionally we might log it as well
                    return BadRequest("Invalid device name");
                }
                SensorType type = (SensorType)sensorType;
                if (sensorData == null || sensorData.Count == 0)
                {
                    logManager.Log(string.Format("Device '{0}' reports empty data", deviceName), LogLevel.Warning);
                    return BadRequest("Invalid device data");
                }
                sensorManagerBll.SaveData(deviceName, type, sensorData);
                return Ok();
            }
            catch (Exception ex)
            {
                logManager.Log("Failed to save data", ex, LogLevel.Error);
                return InternalServerError();
            }
        }

        
    }
}
