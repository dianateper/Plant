using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts
{
    [ServiceContract]
    public interface IContractXam
    {
        [OperationContract]
        string Greeting();
    }
}
