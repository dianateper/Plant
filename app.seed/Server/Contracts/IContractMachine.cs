using Models.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Contracts
{
    [ServiceContract]
    interface IContractMachine
    {
        [OperationContract]
        void SendMachines(List<Machine> machines);

        [OperationContract]
        List<Machine> GetAllMachines();
    }
}
