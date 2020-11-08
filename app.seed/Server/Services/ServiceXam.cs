using Server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class ServiceXam : IContractXam
    {
        public string Greeting()
        {
            return "Hello!";
        }
    }
}
