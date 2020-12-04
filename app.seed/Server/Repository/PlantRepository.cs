using Npgsql;
using Models.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace Server.Repository
{
    class PlantRepository
    {

        PositionRepository positionRepository = new PositionRepository();

        public void AddPlant(Plant plant, int conditionId)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(
            string.Format("INSERT INTO plant(name, price, condition_id) VALUES('" 
            + plant.Name + "', " + plant.Price.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + ", " 
            + conditionId + ");"), DBManager.con);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

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

        public void SetPlantPosition(int plantId, int X, int Y)
        {
            int positionId = positionRepository.GetPositionIdByXAndY(X, Y);
            NpgsqlCommand cmd = new NpgsqlCommand(
                string.Format("INSERT INTO PLANT_POSITION(plant_id, position_id) VALUES(" + plantId+", "+positionId+");"), DBManager.con);
            
          
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void SetPlantHistory(int plantId, int X, int Y, string state)
        {
            int positionId = positionRepository.GetPositionIdByXAndY(X, Y);

            NpgsqlCommand cmd = new NpgsqlCommand(
             string.Format("INSERT INTO PLANTING_HISTORY(plant_id, position_id, datetime, state) VALUES(" + plantId + ", " + positionId
             + ", current_timestamp, '" + state + "');"), DBManager.con);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void DeletePlantFromPosition(int X, int Y)
        {
            int positionId = positionRepository.GetPositionIdByXAndY(X, Y);

            NpgsqlCommand cmd = new NpgsqlCommand(
            string.Format("DELETE FROM PLANT_POSITION WHERE position_id = "  + positionId + ";"), DBManager.con);

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

        public List<Plant> GetPlantsHistoryByPosition(int X, int Y)
        {
            int positionId = positionRepository.GetPositionIdByXAndY(X, Y);
            List<Plant> plants = new List<Plant>();
           
            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT p.plant_id, p.name, h.datetime from PLANT p 
                                        inner join planting_history h 
                                        on p.plant_id=h.plant_id "  +
                                        " where h.position_id=" + positionId + " order by h.datetime desc;", DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Plant plant = new Plant();
                    plant.PlantId = reader.GetInt16(0);
                    plant.Name = reader.GetString(1);
                    plant.datetime = reader.GetDateTime(2);

                    plants.Add(plant);
                }
            }

            return plants;
        }

        public Dictionary<Plant, int> GetPlantFrequency()
        {
            Dictionary<Plant, int> plantFrequency = new Dictionary<Plant, int>();

            NpgsqlCommand cmd = new NpgsqlCommand(
                @"SELECT PLANT.plant_id, PLANT.name, count(*) as cnt
                        from PLANT 
                        inner join PLANT_POSITION on PLANT.plant_id = PLANT_POSITION.plant_id
                           
                        inner join dblink('controller_con', 'SELECT position_id, X, Y FROM Position') 
                            AS ps(position_id int, X int, Y int)

                        on PLANT_POSITION.position_id = ps.position_id GROUP BY PLANT.name, PLANT.plant_id", DBManager.con);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Plant plant = new Plant();
                plant.PlantId = reader.GetInt16(0);
                plant.Name = reader.GetString(1);
                plantFrequency.Add(plant, reader.GetInt32(2));
            }
            reader.Dispose();
            cmd.Dispose();


            return plantFrequency;
        }

        public List<Plant> GetFullPlantedPlants()
        {
            List<Plant> plants = new List<Plant>();
            NpgsqlCommand cmd = new NpgsqlCommand(
                @"SELECT p.plant_id, p.name, p.icon_name, ps.temperature, ps.humidity, c.mintemperature, 
                        c.maxtemperature, c.minhumidity, c.maxhumidity 
                        from PLANT p
                        inner join PLANT_POSITION pp on p.plant_id = pp.plant_id
                        inner join dblink('controller_con', 'SELECT p.position_id, c.temperature, c.humidity 
                            FROM Position p inner join Controller c on p.position_id=c.position_id') 
                            AS ps(position_id int, temperature numeric, humidity numeric)
                        on pp.position_id = ps.position_id
                        inner join condition c on PLANT.condition_id=c.condition_id 
                        where (ps.temperature not between c.mintemperature and c.maxtemperature) 
                        or (ps.humidity not between c.minhumidity and c.maxhumidity)
                        ", DBManager.con);


            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Plant plant = new Plant();
                    plant.PlantId = reader.GetInt16(0);
                    plant.Name = reader.GetString(1);
                    plant.IconName = reader.GetString(2);
                    plant.temperature = reader.GetDouble(3);
                    plant.humidity = reader.GetDouble(4);
                    plant.minTemperature = reader.GetDouble(5);
                    plant.maxTemperature = reader.GetDouble(6);
                    plant.minHumidity = reader.GetDouble(7);
                    plant.maxHumidity = reader.GetDouble(8);
                    plants.Add(plant);
                }
            }
            return plants;
        }

        public Dictionary<Position, Action> CheckPlantsForTemperatureAndHumidity()
        {
            Dictionary<Position, Action> actions = new Dictionary<Position, Action>();
          
            List<Plant> plants = GetFullPlantedPlants();

            plants.ForEach(plant =>
            {
                Action action = Action.None;

                int positionId = positionRepository.GetPositionIdByXAndY(plant.X, plant.Y);

                if (plant.temperature < plant.minTemperature)
                {
                    action = Action.Raise_Temperature;
                }

                if (plant.temperature > plant.maxTemperature)
                {
                    action = Action.Lower_Temperature;
                }

                if (plant.humidity < plant.minHumidity)
                {
                    action = Action.Pour;
                }

                if (plant.humidity > plant.maxHumidity)
                {
                    action = Action.Dry;
                }

                actions.Add(new Position(positionId, plant.X, plant.Y), action);

            });

            return actions;

        }

        public List<Plant> GetFullPlants()
        {
            List<Plant> plants = new List<Plant>();
            NpgsqlCommand cmd = new NpgsqlCommand(
                @"SELECT p.plant_id, p.name, p.icon_name, c.minteperature, 
                        c.maxtemperature, c.minhumidity, c.maxhumidity, c.phmin, c.phmax, s.name 
                        from PLANT p 
                        left join condition c on p.condition_id=c.condition_id 
                        left join soil s on c.soil_id=s.soil_id
                        ", DBManager.con);
            

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Plant plant = new Plant();
                    plant.PlantId = reader.GetInt16(0);
                    plant.Name = reader.GetString(1);
                    plant.IconName = reader.GetString(2);
                    plant.minTemperature = reader.GetDouble(3);
                    plant.maxTemperature = reader.GetDouble(4);
                    plant.minHumidity = reader.GetDouble(5);
                    plant.maxHumidity = reader.GetDouble(6);
                    plant.phMin = reader.GetDouble(7);
                    plant.phMax = reader.GetDouble(8);
                    plant.soil = reader.GetString(9);
                    plants.Add(plant);
                }
            }
            return plants;
        }

        public List<Pricing> GetSumPlantsByMonth()
        {
          
            List<Pricing> pricings = new List<Pricing>();
            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT  sum(p.price), sum(f.price), date_trunc('month', h.datetime) as date from PLANT p 
                                        inner join planting_history h on p.plant_id=h.plant_id  
                                        inner join condition c on p.condition_id=c.condition_id 
                                        inner join condition_fertilizer cf on cf.condition_id=c.condition_id
                                        inner join fertilizer f on f.fertilizer_id=cf.fertilizer_id " +
                                     "group by date_trunc('month', h.datetime);", DBManager.con);
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Pricing price = new Pricing();
                    price.PriceForPlant = reader.GetDouble(0);
                    price.PriceForFertilizer = reader.GetDouble(1);
                    price.Month = reader.GetDateTime(2).ToString("MMMM", CultureInfo.InvariantCulture);
                    pricings.Add(price);
                }
            }

            return pricings;
        }

    }
}
