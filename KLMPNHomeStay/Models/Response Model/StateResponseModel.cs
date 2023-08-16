using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class StateResponseModel
    {
        public string stateId { get; set; }
        public int stateCode { get; set; }
        public string stateName { get; set; }
        public string countryId { get; set; }
        public string countryName { get; set; }
        public short isActive { get; set; }
    }
}
