using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class BookingCancelRequestModel
    {
        public string BookingId { get; set; }
        public string Bname { get; set; }
        public string BranchName { get; set; }
        public string AccNo { get; set; }
        public string AccType { get; set; }
        public string ifsc { get; set; }

        public string CancelReason { get; set; }

    }
}
