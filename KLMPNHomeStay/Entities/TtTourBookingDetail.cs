using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtTourBookingDetail
    {
        public string Id { get; set; }
        public string TourBookingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Sex { get; set; }

        public virtual TtTourBooking TourBooking { get; set; }
    }
}
