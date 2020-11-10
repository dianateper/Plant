using Models.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Contracts
{
    [ServiceContract]
    public interface IContractXam
    {
        [OperationContract]
        string Greeting();

        [OperationContract]
        List<Machine> GetAllMachines();

    }
}
