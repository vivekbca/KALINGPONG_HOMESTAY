using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class BlocakVillage
    {
        public string VillId { get; set; }
        public int VillCode { get; set; }
        public string Village { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public short isActive { get; set; }

    }
    
}
