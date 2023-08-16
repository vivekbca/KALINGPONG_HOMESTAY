using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class TourCancelRequestModel
    {
        public string bookingId { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string accNo { get; set; }
        public string accType { get; set; }
        public string ifsc { get; set; }
        public string cancelReason { get; set; }
        public string userId { get; set; }
    }
}
