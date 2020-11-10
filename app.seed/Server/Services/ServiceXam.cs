using Models.Model;
using Server.Contracts;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class ServiceXam : IContractXam
    {
        MachineRepository MachineRepository = new MachineRepository();

        public List<Machine> GetAllMachines()
        {
            return MachineRepository.GetAllMachines();
        }


        public string Greeting()
        {
            return "Hello!";
        }
    }
}
