using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class HSRefundListResponseModel
    {
        public string hsId { get; set; }
        public string bookingId { get; set; }
        public string hsName { get; set; }
        public string address { get; set; }
        public string villId { get; set; }
        public string village { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string bookingDt { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public string cancelDt { get; set; }
        public int? paymentAmount { get; set; }
        public byte person { get; set; }
    }
}
