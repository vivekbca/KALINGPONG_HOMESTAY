using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmGuestUser
    {
        public TmGuestUser()
        {
            TtBookingCancelledByNavigation = new HashSet<TtBooking>();
            TtBookingGu = new HashSet<TtBooking>();
            TtTourBookingCancelledByNavigation = new HashSet<TtTourBooking>();
            TtTourBookingGu = new HashSet<TtTourBooking>();
        }

        public string GuId { get; set; }
        public string GuName { get; set; }
        public string GuAddress1 { get; set; }
        public string GuAddress2 { get; set; }
        public string GuAddress3 { get; set; }
        public string GuPincode { get; set; }
        public string GuIdentityProof { get; set; }
        public string GuIdentityNo { get; set; }
        public string GuCity { get; set; }
        public string GuStateId { get; set; }
        public string GuCountryId { get; set; }
        public DateTime GuDob { get; set; }
        public string GuSex { get; set; }
        public string GuPassword { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string GuMobileNo { get; set; }
        public string GuEmailId { get; set; }
        public DateTime GuCreatedOn { get; set; }
        public DateTime GuLastActivity { get; set; }
        public short GuIsActive { get; set; }

        public virtual TmCountry GuCountry { get; set; }
        public virtual TmState GuState { get; set; }
        public virtual ICollection<TtBooking> TtBookingCancelledByNavigation { get; set; }
        public virtual ICollection<TtBooking> TtBookingGu { get; set; }
        public virtual ICollection<TtTourBooking> TtTourBookingCancelledByNavigation { get; set; }
        public virtual ICollection<TtTourBooking> TtTourBookingGu { get; set; }
    }
}
