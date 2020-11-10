using System.Collections.Generic;
using System.ServiceModel;
using Models.Model;

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
