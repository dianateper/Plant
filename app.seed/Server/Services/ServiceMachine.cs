using Server.Contracts;
using Server.Model;
using Server.Repository;
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
        MachineRepository MachineRepository = new MachineRepository();
        
        public List<Machine> GetAllMachines()
        {
            return MachineRepository.GetAllMachines();
        }

        public void SendMachines(List<Machine> machines)
        {
            machines.ForEach(m =>
            {
                MachineRepository.SendMachines(m);
            });
        }
    }
}
