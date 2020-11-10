using Models.Model;
using Server.Contracts;
using Server.Repository;
using System.Collections.Generic;

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
