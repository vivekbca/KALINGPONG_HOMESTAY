using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmHomestay
    {
        public TmHomestay()
        {
            TmHsRooms = new HashSet<TmHsRooms>();
            TtBankTransaction = new HashSet<TtBankTransaction>();
            TtBooking = new HashSet<TtBooking>();
            TtBookingRoomDetail = new HashSet<TtBookingRoomDetail>();
        }

        public string HsId { get; set; }
        public string HsName { get; set; }
        public string HomestayDescription { get; set; }
        public string LocalAttraction { get; set; }
        public string HwtReach { get; set; }
        public string HsAddress1 { get; set; }
        public string HsAddress2 { get; set; }
        public string HsAddress3 { get; set; }
        public string Pincode { get; set; }
        public string AddonServices { get; set; }
        public string VillageCategoryId { get; set; }
        public string HsVillId { get; set; }
        public string HsBlockId { get; set; }
        public string HsDistrictId { get; set; }
        public string HsStateId { get; set; }
        public string HsCountryId { get; set; }
        public string DestinationId { get; set; }
        public string UserId { get; set; }
        public string HsContactName { get; set; }
        public string HsContactMob1 { get; set; }
        public string HsContactMob2 { get; set; }
        public string HsContactEmail { get; set; }
        public int HsNoOfRooms { get; set; }
        public string HsBankName { get; set; }
        public string HsBankBranch { get; set; }
        public int HsAccountNo { get; set; }
        public string HsAccountType { get; set; }
        public string HsIfsc { get; set; }
        public string HsMicr { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public short IsActive { get; set; }
        public DateTime ActiveSince { get; set; }
        public string DeactivatedBy { get; set; }
        public DateTime? DeactivatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual TmUser ApprovedByNavigation { get; set; }
        public virtual TmUser CreatedByNavigation { get; set; }
        public virtual TmUser DeactivatedByNavigation { get; set; }
        public virtual TmPopulardestination Destination { get; set; }
        public virtual TmBlock HsBlock { get; set; }
        public virtual TmCountry HsCountry { get; set; }
        public virtual TmDistrict HsDistrict { get; set; }
        public virtual TmState HsState { get; set; }
        public virtual TmBlockVillage HsVill { get; set; }
        public virtual TmUser ModifiedByNavigation { get; set; }
        public virtual TmUser User { get; set; }
        public virtual TmVillageCategory VillageCategory { get; set; }
        public virtual TmHsGallery TmHsGallery { get; set; }
        public virtual TtHsPopularity TtHsPopularity { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRooms { get; set; }
        public virtual ICollection<TtBankTransaction> TtBankTransaction { get; set; }
        public virtual ICollection<TtBooking> TtBooking { get; set; }
        public virtual ICollection<TtBookingRoomDetail> TtBookingRoomDetail { get; set; }
    }
}
