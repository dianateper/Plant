using Server.Contracts;
using Server.Model;
using Server.Repository;
using System.Collections.Generic;
using System.Windows;

namespace Server.Services
{

    class ServiceWeb : IContractWeb
    {
        PlantRepository plantRepository = new PlantRepository();

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

        public string Greeting()
        {
            return "Hello!";
        }

        public void SetPlants(List<Plant> plants)
        {
            plants.ForEach(plant => plantRepository.SetPlantPosition(plant.PlantId, plant.X, plant.Y));
        }
    }
}
