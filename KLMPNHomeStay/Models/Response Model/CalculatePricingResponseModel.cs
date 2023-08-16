using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class CalculatePricingResponseModel
    {
        public int adultNo { get; set; }
        public int childNo { get; set; }
        public int discountRate { get; set; }
        public int totalRoomRate { get; set; }
        public int totalBillRate { get; set; }
        public int withOutDiscountTotal { get; set; }
        public int discountAmount { get; set; }
    }
}
