using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts
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
