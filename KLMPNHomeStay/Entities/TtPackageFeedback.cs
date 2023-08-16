using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtPackageFeedback
    {
        public string FeedbackId { get; set; }
        public string TourBookingId { get; set; }
        public int? HsRatings { get; set; }
        public string HsFeedback { get; set; }
        public short IsViewed { get; set; }
        public short IsActionTaken { get; set; }
        public string ActionDescription { get; set; }
        public string ActionTakenBy { get; set; }
        public DateTime? ActionDate { get; set; }

        public virtual TmUser ActionTakenByNavigation { get; set; }
        public virtual TtTourBooking TourBooking { get; set; }
    }
}
