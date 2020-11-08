using Server.Contracts;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Server.Services
{
    class ServiceController : IContractControllers
    {
        public void SendControllers(List<Controller> controllers)
        {
            controllers.ForEach(c =>
            {
                MessageBox.Show(c.controllerId + " " + c.X + " " + c.Y);
            });
        }
    }
}
