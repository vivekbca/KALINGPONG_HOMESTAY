using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmTour
    {
        public TmTour()
        {
            TtTourBooking = new HashSet<TtTourBooking>();
            TtTourDate = new HashSet<TtTourDate>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public int DestinationDay { get; set; }
        public int DestinationNight { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonNameEmail { get; set; }
        public int? ContactPersonMobile { get; set; }
        public string FacilityId1 { get; set; }
        public string FacilityId2 { get; set; }
        public string FacilityId3 { get; set; }
        public string FacilityId4 { get; set; }
        public string FacilityId5 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string TourPdfFile { get; set; }
        public short IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual TmUser CreatedByNavigation { get; set; }
        public virtual TmHsFacilities FacilityId1Navigation { get; set; }
        public virtual TmHsFacilities FacilityId2Navigation { get; set; }
        public virtual TmHsFacilities FacilityId3Navigation { get; set; }
        public virtual TmHsFacilities FacilityId4Navigation { get; set; }
        public virtual TmHsFacilities FacilityId5Navigation { get; set; }
        public virtual ICollection<TtTourBooking> TtTourBooking { get; set; }
        public virtual ICollection<TtTourDate> TtTourDate { get; set; }
    }
}
