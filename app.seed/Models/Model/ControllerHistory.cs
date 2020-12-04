using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class ControllerHistory
    {
        public int controllerHistoryId { get; set; }

        public double temperature { get; set; }

        public double humidity { get; set; }

        public DateTime datetime { get; set; }

    }
}

