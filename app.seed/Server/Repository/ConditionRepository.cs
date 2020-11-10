using Npgsql;
using Models.Model;

namespace Server.Repository
{
    class ConditionRepository
    {
        public Condition GetConditionByPlant(int plant)
        {
           
            NpgsqlCommand cmd = new NpgsqlCommand(
                @"SELECT condition_id, soil, minTeperature, maxTeperature, minHumidity, maxHumidity  
                    FROM (SELECT c.condition_id, s.name, c.minTeperature, c.maxTeperature, c.minHumidity, c.maxHumidity  
                    from CONDITION c left join SOIL s on s.soil_id = c.soil_id LEFT JOIN PLANT p on c.condition_id = p.consition_id 
                    where p.plant_id = @plantId') 
                    AS t(condition_id int, soil varchar, minTeperature numeric, maxTeperature numeric, minHumidity numeric, maxHumidity numeric)", DBManager.con);

            cmd.Parameters.AddWithValue("@plantId", plant);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            Condition condition = new Condition();
            
            condition.ConditionId = int.Parse(reader["condition_id"].ToString());
            
            condition.Soil = reader["soil"].ToString();
            condition.MinTmp = double.Parse(reader["minTeperature"].ToString());
            condition.MaxTmp = double.Parse(reader["maxTeperature"].ToString());
            condition.MinHumidity = double.Parse(reader["minHumidity"].ToString());
            condition.MaxHumidity = double.Parse(reader["maxHumidity"].ToString());

            reader.Dispose();
            cmd.Dispose();
            
            return condition;
        }

    }
}
