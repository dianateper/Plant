using Server.Contracts;
using Server.Model;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Server.Services
{
    class ServiceController : IContractControllers
    {

        ControllerRepository ControllerRepository = new ControllerRepository();

        public List<Controller> GetAllControllers()
        {
            return ControllerRepository.GetAllControllers();
        }

        public void SendControllers(List<Controller> controllers)
        {
            controllers.ForEach(c => { ControllerRepository.SetTempAndHumidity(c); });
        }
    }
}
