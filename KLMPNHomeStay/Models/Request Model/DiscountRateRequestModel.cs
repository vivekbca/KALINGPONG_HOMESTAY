using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class DiscountRateRequestModel
    {
        //public int adultNo { get; set; }
        public int discountRate { get; set; }
        public string discountType { get; set; }
        public int totalAmount { get; set; }
    }
}
