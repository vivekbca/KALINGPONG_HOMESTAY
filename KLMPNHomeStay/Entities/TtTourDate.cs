using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtTourDate
    {
        public TtTourDate()
        {
            TtTourBooking = new HashSet<TtTourBooking>();
        }

        public string Id { get; set; }
        public string TourId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public short IsActive { get; set; }

        public virtual TmTour Tour { get; set; }
        public virtual ICollection<TtTourBooking> TtTourBooking { get; set; }
    }
}
