using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class TourRefundBankRequestModel
    {
        public string bookingId { get; set; }
        public string voucherMode { get; set; }
        public string voucherStatus { get; set; }
        public string voucherNo { get; set; }
        public string voucherDt { get; set; }
        public string refundBy { get; set; }
    }
}
