using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Server.Model;

namespace Controllers
{
    [ServiceContract]
    interface IContractControllers
    {
        [OperationContract]
        void SendControllers(List<Controller> controllers);

        [OperationContract]
        List<Controller> GetAllControllers();
    }
}
