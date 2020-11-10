using Models.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace app.seed
{
    [ServiceContract]
    public interface IContractXam
    {
        [OperationContract]
        string Greeting();

        [OperationContract]
        List<Machine> GetAllMachines();

        [OperationContract]
        LinkedList<Position> GetPath(Position start, Position end);
    }

}
