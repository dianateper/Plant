using Models.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace app.seed
{
    [ServiceContract]
    public interface IContractXam
    {

        [OperationContract]
        List<Machine> GetAllMachines();

        [OperationContract]
        List<Position> GetAllPositions();

        [OperationContract]
        LinkedList<Position> GetOptimalRoute(Position start, Position end);

        [OperationContract]
        bool ChangeMachinePosition(Machine machine);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Controller GetControllerByPosition(int X, int Y);

        [OperationContract]
        List<Controller> GetAllControllers();
    }

}
