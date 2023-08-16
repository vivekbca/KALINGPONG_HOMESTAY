using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class FeedbackResponseModel
    {
        public string FeedbackId { get; set; }
        public string HsBookingId { get; set; }
        public int? HsRatings { get; set; }
        public string HsFeedback { get; set; }
        public short IsViewed { get; set; }
        public short IsActionTaken { get; set; }
        public string ActionDescription { get; set; }
        public string ActionTakenBy { get; set; }
        public string ActionDate { get; set; }
    }
}
