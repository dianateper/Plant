using Models.Model;
using Npgsql;
using System.Collections.Generic;

namespace Server.Repository
{
    class FertilizerRepository
    {


        public int AddFertilizer(Fertilizer fertilizer)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(

             string.Format("INSERT INTO FERTILIZER(name)" +
                " VALUES(" + fertilizer.Name + "') RETURNING fertilizer_id;"), DBManager.con);

            int fertilizerId = 0;
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    fertilizerId = reader.GetInt32(0);
                }
            }
            
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            return fertilizerId;
        }

        public void AddFertilizerCondition(int fertilizerId,int conditionId, int count)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(

             string.Format("INSERT INTO CONDITION_FERTILIZER(fertilizer_id, condition_id, count)" +
                " VALUES(" + fertilizerId + ", " + conditionId + "," + count + ");"), DBManager.con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }


        public List<Fertilizer> GetAllFertilizer()
        {
            List<Fertilizer> fertilizers = new List<Fertilizer>();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT fertilizer_id, name from FERTILIZER", DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Fertilizer fertilizer = new Fertilizer();
                    fertilizer.FertilizerId = reader.GetInt16(0);
                    fertilizer.Name = reader.GetString(1);
                    fertilizers.Add(fertilizer);
                }
            }
            return fertilizers;
        }


        public List<Fertilizer> GetFertilizersByPlantId(int plantId)
        {
            List<Fertilizer> fertilizers = new List<Fertilizer>();
            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT f.fertilizer_id, f.name, cf.count from FERTILIZER f
                    inner join condition_fertilizer cf on f.fertilizer_id=cf.fertilizer_id 
                    inner join condition c on cf.condition_id=c.condition_id 
                    inner join plant p on p.condition_id=c.condition_id where p.plant_id=" + plantId+";", DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Fertilizer fertilizer = new Fertilizer();
                    fertilizer.FertilizerId = reader.GetInt16(0);
                    fertilizer.Name = reader.GetString(1);
                    fertilizer.Count = reader.GetInt16(2);
                    fertilizers.Add(fertilizer);
                }
            }
            return fertilizers;
        }

     

    }
}
