using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class CountryResponseModel
    {
        public string countryId { get; set; }
        public int countryCode { get; set; }
        public string countryName { get; set; }
        public short isActive { get; set; }
    }
}
