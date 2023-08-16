using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class CalculatePricingRequestModel
    {
        public int adultNo { get; set; }
        public int childNo { get; set; }
        public int discountRate { get; set; }
        public int totalRate { get; set; }

    }
}
