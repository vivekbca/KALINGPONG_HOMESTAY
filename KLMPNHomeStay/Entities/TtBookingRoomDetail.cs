using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtBookingRoomDetail
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public string HsId { get; set; }
        public DateTime FromDt { get; set; }
        public DateTime? ToDt { get; set; }
        public byte? RoomNo { get; set; }

        public virtual TtBooking Booking { get; set; }
        public virtual TmHomestay Hs { get; set; }
    }
}
