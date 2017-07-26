using Ninject.Modules;
using Ninject.Web.Common;
using Eaton.Homework.Business;

namespace Eaton.Homework.Monitor.App_Start
{
    public class MonitorBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogManager>().To<LogManager>().InSingletonScope();
            Bind<ISensorManager>().To<SensorManager>();
        }
    }
}