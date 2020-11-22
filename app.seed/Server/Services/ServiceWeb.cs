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
        FertilizerRepository fertilizer = new FertilizerRepository();

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
            return fertilizer.GetFertilizersByPlantId(plantId);
        }

        public List<Plant> GetListFullPlants()
        {
            return plantRepository.GetFullPlants();
        }
    }
}
