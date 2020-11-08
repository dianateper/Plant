﻿using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Machines
{
    [ServiceContract]
    interface IContractMachine
    {
        [OperationContract]
        void SendMachines(List<Machine> machines);
    }
}