using Models.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository
{
    class MachineRepository
    {
        PositionRepository positionRepository = new PositionRepository();

        public List<Machine> GetAllMachines()
        {
            List<Machine> machines = new List<Machine>();

            NpgsqlCommand cmd = new NpgsqlCommand(string.Format(@"SELECT machine_id, name, type, X, Y FROM 
                                dblink('{0}','SELECT m.machine_id, m.name, t.name, p.X, p.Y from machine m inner join machine_type t 
                                    on m.machine_type_id = t.machine_type_id
                                    inner join position p on m.position_id = p.position_id
                                    ') 
                                AS t(machine_id int, name varchar, type varchar, X int, Y int)", DBManager.DBController), DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Machine machine = new Machine();
                    machine.machineId = reader.GetInt16(0);
                    machine.Name = reader.GetString(1);
                    machine.Type = reader.GetString(2);
                    machine.X = reader.GetInt16(3);
                    machine.Y = reader.GetInt16(4);
                    machines.Add(machine);
                }
            }

            return machines;
        }


        public void SendMachines(Machine machine)
        {

            int positionId = positionRepository.GetPositionIdByXAndY(machine.X, machine.Y);

            NpgsqlCommand cmd = new NpgsqlCommand(string.Format(
              @"SELECT dblink('{0}','INSERT INTO Machine(position_id) VALUES("
                                  + positionId + ") WHERE machine_id="
                                  + machine.machineId + ";')", DBManager.DBController), DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader()) { }
        }

        public bool SetNewMachineLocation(Machine machine)
        {

            int positionId = positionRepository.GetPositionIdByXAndY(machine.X, machine.Y);

            NpgsqlCommand cmd = new NpgsqlCommand(string.Format(
                "UPDATE Machine SET X = " + machine.X +
                ", Y=" + machine.Y +                
                " WHERE machine_id = " + machine.machineId + ";')", DBManager.DBController), DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader()) { }

            return true;
        }

    }
}
