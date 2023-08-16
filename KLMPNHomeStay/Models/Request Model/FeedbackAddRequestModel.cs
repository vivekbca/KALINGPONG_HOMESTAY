using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class FeedbackAddRequestModel
    {
       // public string FeedbackId { get; set; }
        public string HsBookingId { get; set; }
        public string HsRatings { get; set; }
        public string HsFeedback { get; set; }
        //public short IsViewed { get; set; }
        //public short IsActionTaken { get; set; }
        //public string ActionDescription { get; set; }
        //public string ActionTakenBy { get; set; }
        //public string ActionDate { get; set; }
    }
}
