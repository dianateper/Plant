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
    interface IContractMachine
    {
        [OperationContract]
        void SendMachines(List<Machine> machines);

        [OperationContract]
        List<Machine> GetAllMachines();
    }
}
