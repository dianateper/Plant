using Server.Contracts;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Server.Services
{
    class ServiceMachine : IContractMachine
    {
        public void SendMachines(List<Machine> machines)
        {
            machines.ForEach(m => { MessageBox.Show(m.machineId + " " + m.X + " " + m.Y); });
        }
    }
}
