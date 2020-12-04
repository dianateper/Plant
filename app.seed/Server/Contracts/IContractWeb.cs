using Models.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Contracts
{
    [ServiceContract]
    interface IContractWeb
    {
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [ServiceKnownType(typeof(Plant))]
        List<Plant> GetListPlants();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [ServiceKnownType(typeof(Plant))]
        List<Plant> GetListFullPlants();

        [OperationContract]
        [ServiceKnownType(typeof(Plant))]
        [FaultContract(typeof(FaultException))]
        void SetPlants(List<Plant> plants);

        [OperationContract]
        [ServiceKnownType(typeof(Plant))]
        [FaultContract(typeof(FaultException))]
        List<Plant> GetPlantedSeeds();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        ControllerStatistica GetControllerStatistica(int X, int Y);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<Plant> GetPlantsHistoryByPosition(int X, int Y);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Controller GetControllerByPosition(int X, int Y);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Dictionary<Plant, int> GetFieldStatistic();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<Fertilizer> GetFeritilizerByPlantId(int plantId);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<Fertilizer> GetAllFertilizer();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<Soil> GetAllSoils();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<double> MakeArimaPrediction(int forecast, List<double> parameters);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<Pricing> GetPrice();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        void AddPlant(Plant plant, Condition condition, Soil soil, Fertilizer fertilizer);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        void AddSoil(Soil soil);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        void AddFertilizer(Fertilizer fertilizer);


    }
}
