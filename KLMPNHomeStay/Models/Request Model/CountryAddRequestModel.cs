using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class CountryAddRequestModel
    {
        public string countryId { get; set; }
        public int countryCode { get; set; }
        public string countryName { get; set; }
        public short isActive { get; set; }
    }
}
