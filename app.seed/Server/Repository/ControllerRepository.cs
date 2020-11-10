using Npgsql;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Data;
using System.Globalization;

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
               @"SELECT dblink('{0}','UPDATE controller SET temperature={1} , humidity={2} WHERE controller_id={3}')", DBManager.DBController, 
               controller.temperature.ToString("0.0", CultureInfo.GetCultureInfo("en-US")), controller.humidity.ToString("0.0", CultureInfo.GetCultureInfo("en-US")), controller.ControllerId), DBManager.con);
           

            using (NpgsqlDataReader reader = cmd.ExecuteReader()){}
    
        }

        public void SetHistory(Controller controller)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(string.Format(
                  @"SELECT dblink('{0}','INSERT INTO controller_history(controller_id, temperature, humidity, datetime) VALUES("
                                      + controller.ControllerId + ", " + controller.temperature.ToString("0.0", CultureInfo.GetCultureInfo("en-US"))
                                      + " ," + controller.humidity.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + ", current_timestamp);')", DBManager.DBController), DBManager.con);

            using (NpgsqlDataReader reader = cmd.ExecuteReader()) { }
        }




            public int GetContollerIdByPosition(int X, int Y)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(
                string.Format("SELECT controller_id FROM dblink('{0}','SELECT c.controller_id from CONTROLLER c inner join POSITION p on c.position_id=p.position_id where p.x=" + X + " and p.y=" + Y + "') AS t(controller_id int)", DBManager.DBController), DBManager.con);

            int rs;
            using (NpgsqlDataReader result = cmd.ExecuteReader())
            {
                result.Read();
                rs = int.Parse(result["controller_id"].ToString());
            }

            return rs;
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
