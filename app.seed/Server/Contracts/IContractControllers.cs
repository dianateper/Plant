using Models.Model;
using System.Collections.Generic;
using System.ServiceModel;

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
