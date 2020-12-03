using Npgsql;
using Models.Model;
using System.Collections.Generic;
using System.Globalization;

namespace Server.Repository
{
    class ConditionRepository
    {

        public void AddSoil(Soil soil)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(

            string.Format("INSERT INTO SOIL(name) VALUES(" +soil.Name+ "');"), DBManager.con);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public int AddCondition(Condition condition, int soilId)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(

            string.Format("INSERT INTO CONDITION(soil_id, minteperature, maxtemperature, minhumidity, maxhumidity, phmin, phmax) " +
            " VALUES(" + soilId + "," + condition.MinTmp.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + ", " + condition.MaxTmp.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + "," 
            + condition.MinHumidity.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + ","  + condition.MaxHumidity.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + "," 
            + condition.phMin.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + "," + condition.phMax.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + " ) RETURNING condition_id;"), DBManager.con);

            object res = cmd.ExecuteScalar();
            return int.Parse(res.ToString());
        }

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

        public List<Soil> GetAllSoil()
        {
            List<Soil> soils = new List<Soil>();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT soil_id, name from SOIL", DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Soil soil = new Soil();
                    soil.SoilId = reader.GetInt32(0);
                    soil.Name = reader.GetString(1);
                    soils.Add(soil);
                }
            }

            return soils;
        }

    }
}
