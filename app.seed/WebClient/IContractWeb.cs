using Models.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace WebClient
{
    [ServiceContract]
    public interface IContractWeb
    {
        [OperationContract]
        string Greeting();

        [OperationContract]
        List<Plant> GetListPlants();


        [OperationContract]
        void SetPlants(List<Plant> plants);

        [OperationContract]
        List<Plant> GetPlantedSeeds();
       
        [OperationContract]
        ControllerStatistica GetControllerStatistica(int X, int Y);

        [OperationContract]
        List<Plant> GetPlantsHistoryByPosition(int X, int Y);

        [OperationContract]
        Controller GetControllerByPosition(int X, int Y);

        [OperationContract]
        Dictionary<Plant, int> GetFieldStatistic();

        [OperationContract]
        List<Fertilizer> GetFeritilizerByPlantId(int plantId);

        [OperationContract]
        List<Plant> GetListFullPlants();

    }

}
