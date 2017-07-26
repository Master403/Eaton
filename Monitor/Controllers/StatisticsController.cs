using System;
using System.Linq;
using System.Web.Http;
using Eaton.Homework.Business;
using Eaton.Homework.Monitor.Models;

namespace Eaton.Homework.Monitor.Controllers
{
    public class StatisticsController : ApiController
    {
        /// <summary>Contains business logic related to <see cref="Sensor"/> objects.</summary>
        private readonly ISensorManager sensorManagerBll;
        /// <summary>Contains methods to log errors</summary>
        private readonly ILogManager logManager;

        /// <summary>Initializes a new instance of the <see cref="DeviceController"/> class.</summary>
        /// <param name="sensorManagerBll">Contains business logic related to <see cref="Sensor"/> objects.</param>
        /// <param name="logManager">Contains method to log erros</param>
        public StatisticsController(ISensorManager sensorManagerBll, ILogManager logManager)
        {
            if (sensorManagerBll == null) throw new ArgumentNullException(nameof(sensorManagerBll));
            if (logManager == null) throw new ArgumentNullException(nameof(logManager));

            this.sensorManagerBll = sensorManagerBll;
            this.logManager = logManager;
        }

        [HttpGet]
        public IHttpActionResult Statistics()
        {
            try
            {
                var statistics = sensorManagerBll.GetDeviceStatistics()
                    .Select(x => new DeviceStatisticsModel { DeviceName = x.DeviceName, MessageCount = x.MessageCount });
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                logManager.Log("Failed to load devices", ex, LogLevel.Error);
                return InternalServerError();
            }
        }
       
    }
}
