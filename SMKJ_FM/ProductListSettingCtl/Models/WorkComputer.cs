using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class WorkComputer
    {
        public string ID;

        public string WorkComputerName
        {
            get;
            set;
        }

        public string ComputerIP
        {
            get;
            set;
        }

        public string HardwareConfig
        {
            get;
            set;
        }

        public string CreateDate
        {
            get;
            set;
        }
    }
}