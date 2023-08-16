using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class StateAddRequestModel
    {
        public string stateId { get; set; }
        public int stateCode { get; set; }
        public string stateName { get; set; }
        public string countryId { get; set; }
        public short isActive { get; set; }
    }
}
