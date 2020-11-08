﻿using Npgsql;
using Server.Model;
using System.Collections.Generic;
using System.Windows;

namespace Server.Repository
{
    class PlantRepository
    {

        PositionRepository positionRepository = new PositionRepository();

        public List<Plant> GetAllPlants()
        {
            List<Plant> plants = new List<Plant>();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT plant_id, name, icon_name from PLANT", DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Plant plant = new Plant();
                    plant.PlantId = reader.GetInt16(0);
                    plant.Name = reader.GetString(1);
                    plant.IconName = reader.GetString(2);
                    plants.Add(plant);
                }
            }
          
            return plants;
        }

        public void SetPlantPosition(int plantId, int x, int y)
        {
            int positionId = positionRepository.GetPositionIdByXAndY(x, y);

            NpgsqlCommand cmd = new NpgsqlCommand(
                string.Format("INSERT INTO PLANT_POSITION(plant_id, position_id) VALUES(" + plantId+", "+positionId+");"), DBManager.con);
  
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public List<Plant> GetPlantedPlants()
        {
            List<Plant> plants = new List<Plant>();

            NpgsqlCommand cmd = new NpgsqlCommand(
                @"SELECT PLANT.plant_id, PLANT.name, PLANT.icon_name, ps.X, ps.Y 
                        from PLANT 
                        inner join PLANT_POSITION on PLANT.plant_id = PLANT_POSITION.plant_id
                           
                        inner join dblink('controller_con', 'SELECT position_id, X, Y FROM Position') 
                            AS ps(position_id int, X int, Y int)

                        on PLANT_POSITION.position_id = ps.position_id", DBManager.con);

            NpgsqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                Plant plant = new Plant();
                plant.PlantId = reader.GetInt16(0);
                plant.Name = reader.GetString(1);
                plant.IconName = reader.GetString(2);
                plant.X = reader.GetInt16(3);
                plant.Y = reader.GetInt16(4);
                plants.Add(plant);
            }
            reader.Dispose();
            cmd.Dispose();
         
            return plants;
        }

    }
}