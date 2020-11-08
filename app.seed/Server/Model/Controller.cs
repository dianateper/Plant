﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public class Controller
    {
        public int controllerId { get; set; }

        public double temperature { get; set; }

        public double humidity { get; set; }

        public DateTime date { get; set; }

        public int positionId { get; set; }

    }
}
