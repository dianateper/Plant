using Npgsql;
using Server.Model;
using System;
using System.Collections.Generic;

namespace Server.Repository
{
    class ControllerRepository
    { 
        public List<Controller> GetAllControllers()
        {
            List<Controller> controllers = new List<Controller>();

            NpgsqlCommand cmd = new NpgsqlCommand(string.Format(@"SELECT controller_id, position_id FROM 
                                dblink('{0}','SELECT controller_id, position_id from controller') 
                                AS t(controller_id int, position_id int)", DBManager.DBController), DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Controller controller = new Controller();
                    controller.ControllerId = reader.GetInt16(0);
                    controller.PositionId = reader.GetInt16(1);
                    controllers.Add(controller);
                }
            }

            return controllers;
        }

        public void SetTempAndHumidity(Controller controller)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(string.Format(
                @"SELECT dblink('{0}','INSERT INTO controller(temperature, humidity) VALUES("
                                    +controller.temperature + " ," + controller.humidity + ") WHERE controller_id="
                                    +controller.ControllerId + ";')", DBManager.DBController), DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader()){}

            cmd = new NpgsqlCommand(string.Format(
                @"SELECT dblink('{0}','INSERT INTO controller_history(controller_id, temperature, humidity, datetime) VALUES("
                                    + controller.ControllerId + " ," + controller.temperature + " ," + controller.humidity + " ," 
                                    + DateTime.Now + ");')", DBManager.DBController), DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader()) { }

        }

        public List<ControllerHistory> GetControllerHistory(int controller_id)
        {
            List<ControllerHistory> controllerHistories = new List<ControllerHistory>();

            NpgsqlCommand cmd = new NpgsqlCommand(string.Format(
                @"SELECT controller_history_id, temperature, humidity, datetime FROM 
                                dblink('{0}',
                                'SELECT controller_history_id, temperature, humidity, datetime 
                                from controller_history
                                where controller_id="+controller_id+";') AS t(controller_history_id int, temperature numeric, humidity numeric, datetime date)", DBManager.DBController), 
                                DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ControllerHistory history = new ControllerHistory();
                    history.controllerHistoryId = reader.GetInt32(0);
                    history.temperature = reader.GetDouble(1);
                    history.humidity = reader.GetDouble(2);
                    history.datetime = reader.GetDateTime(3);
                    controllerHistories.Add(history);
                    
                }
            }

            return controllerHistories; 
        }
     

    }
}
