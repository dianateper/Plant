using Npgsql;
using Models.Model;
using System.Collections.Generic;
using System.Linq;

namespace Server.Repository
{
    class PositionRepository
    {

        public List<Position> GetAllPosition()
        {
            List<Position> positions = new List<Position>();
            NpgsqlCommand cmd = new NpgsqlCommand(string.Format("SELECT position_id, x, y FROM dblink('{0}','SELECT position_id, x, y from POSITION') AS t(position_id int, x int, y int)", DBManager.DBController), DBManager.con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Position position = new Position();
                position.PositionId = reader.GetInt16(0);
                position.X = reader.GetInt16(1);
                position.Y = reader.GetInt16(2);

                positions.Add(position);
            }
            reader.Dispose();
            cmd.Dispose();

            return positions;
        }

        public int GetPositionIdByXAndY(int x, int y)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(
                string.Format("SELECT position_id FROM dblink('{0}','SELECT position_id from POSITION where x="+x+" and y="+y+"') AS t(position_id int)", DBManager.DBController), DBManager.con);

            int rs;
            using (NpgsqlDataReader result = cmd.ExecuteReader())
            {
                result.Read();
                rs = int.Parse(result["position_id"].ToString());
            }
            return rs;
        }

    }
}
