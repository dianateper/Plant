using Models.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Contracts
{
    [ServiceContract]
    interface IContractControllers
    {
        [OperationContract]
        void SendControllers(DateTime date, List<Controller> controllers);

        [OperationContract]
        List<Controller> GetAllControllers();
    }
}
