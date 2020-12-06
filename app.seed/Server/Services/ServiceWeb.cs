using Server.Contracts;
using Models.Model;
using Server.Repository;
using System.Collections.Generic;
using System.Windows;

namespace Server.Services
{

    class ServiceWeb : IContractWeb
    {
        PlantRepository plantRepository = new PlantRepository();
        ControllerRepository controllerRepository = new ControllerRepository();
        FertilizerRepository fertilizerRepository = new FertilizerRepository();
        ConditionRepository conditionRepository = new ConditionRepository();

        public List<Plant> GetListPlants()
        {
            List<Plant> plants = plantRepository.GetAllPlants();
            return new List<Plant>(plants);
        }

        public List<Plant> GetPlantedSeeds()
        {
           
            List<Plant> plants = plantRepository.GetPlantedPlants();
            return new List<Plant>(plants);
        }

        public void SetPlants(List<Plant> plants)
        {
            plants.ForEach(plant => plantRepository.SetPlantPosition(plant.PlantId, plant.X, plant.Y));
            plants.ForEach(plant => plantRepository.SetPlantHistory(plant.PlantId, plant.X, plant.Y, "Planted"));
        }

        public ControllerStatistica GetControllerStatistica(int X, int Y)
        {
            int controllerId = controllerRepository.GetContollerIdByPosition(X, Y);
            List<ControllerHistory> histories = controllerRepository.GetControllerHistory(controllerId);
            ControllerStatistica controllerStatistica = new ControllerStatistica(histories);

            return controllerStatistica;

        }

        public List<Plant> GetPlantsHistoryByPosition(int X, int Y)
        {
            return plantRepository.GetPlantsHistoryByPosition(X,Y);
        }

        public Controller GetControllerByPosition(int X, int Y)
        {
            return controllerRepository.GetControllerByXAndY(X, Y);
        }

        public Dictionary<Plant, int> GetFieldStatistic()
        {
            return plantRepository.GetPlantFrequency();
        }

        public List<Fertilizer> GetFeritilizerByPlantId(int plantId)
        {
            return fertilizerRepository.GetFertilizersByPlantId(plantId);
        }

        public List<Plant> GetListFullPlants()
        {
            return plantRepository.GetFullPlants();
        }

        public List<double> MakeArimaPrediction(int forecast, List<double> parameters)
        {
            Arima.Arima a = new Arima.Arima(parameters.ToArray(), forecast, 1, 1, 1);
            return a.MakePredition();
        }

        public List<Pricing> GetPrice()
        {
            return plantRepository.GetSumPlantsByMonth();
        }

        public List<Fertilizer> GetAllFertilizer()
        {
            return fertilizerRepository.GetAllFertilizer();
        }

        public List<Soil> GetAllSoils()
        {
            return conditionRepository.GetAllSoil();
        }

        public void AddPlant(Plant plant, Models.Model.Condition condition, Soil soil, Fertilizer fertilizer)
        {
            int conditionId = conditionRepository.AddCondition(condition, soil.SoilId);
            fertilizerRepository.AddFertilizerCondition(fertilizer.FertilizerId, conditionId, fertilizer.Count);
            plantRepository.AddPlant(plant, 2);
           
        }

        public void AddSoil(Soil soil)
        {
            conditionRepository.AddSoil(soil);
        }

        public void AddFertilizer(Fertilizer fertilizer)
        {
            fertilizerRepository.AddFertilizer(fertilizer);
        }
    }
}
