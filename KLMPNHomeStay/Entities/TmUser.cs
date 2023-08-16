using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmUser
    {
        public TmUser()
        {
            InverseUserCreatedByNavigation = new HashSet<TmUser>();
            TmBlockCreatedByNavigation = new HashSet<TmBlock>();
            TmBlockModifiedByNavigation = new HashSet<TmBlock>();
            TmBlockVillageCreatedByNavigation = new HashSet<TmBlockVillage>();
            TmBlockVillageModifiedByNavigation = new HashSet<TmBlockVillage>();
            TmCountryCreatedByNavigation = new HashSet<TmCountry>();
            TmCountryModifiedByNavigation = new HashSet<TmCountry>();
            TmDistrictCreatedByNavigation = new HashSet<TmDistrict>();
            TmDistrictModifiedByNavigation = new HashSet<TmDistrict>();
            TmHomestayApprovedByNavigation = new HashSet<TmHomestay>();
            TmHomestayCreatedByNavigation = new HashSet<TmHomestay>();
            TmHomestayDeactivatedByNavigation = new HashSet<TmHomestay>();
            TmHomestayModifiedByNavigation = new HashSet<TmHomestay>();
            TmHomestayUser = new HashSet<TmHomestay>();
            TmHsRoomsCreatedByNavigation = new HashSet<TmHsRooms>();
            TmHsRoomsModifiedByNavigation = new HashSet<TmHsRooms>();
            TmStateCreatedByNavigation = new HashSet<TmState>();
            TmStateModifiedByNavigation = new HashSet<TmState>();
            TmTour = new HashSet<TmTour>();
            TtBankTransaction = new HashSet<TtBankTransaction>();
            TtBooking = new HashSet<TtBooking>();
            TtHsFeedback = new HashSet<TtHsFeedback>();
            TtPackageFeedback = new HashSet<TtPackageFeedback>();
            TtTourBooking = new HashSet<TtTourBooking>();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserRoleId { get; set; }
        public DateTime UserDob { get; set; }
        public string UserSex { get; set; }
        public string UserPassword { get; set; }
        public int UserMobileNo { get; set; }
        public string UserEmailId { get; set; }
        public DateTime UserLastActivity { get; set; }
        public short UserIsActive { get; set; }
        public short IsSystemDefined { get; set; }
        public DateTime? PasswordLastChanged { get; set; }
        public string PreviousPasswords { get; set; }
        public int? InvalidLoginAttempts { get; set; }
        public byte LockoutEnabled { get; set; }
        public DateTime? LockedOutUntil { get; set; }
        public DateTime? LastLogin { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime UserCreatedOn { get; set; }
        public string UserCreatedBy { get; set; }

        public virtual TmUser UserCreatedByNavigation { get; set; }
        public virtual TmUserRole UserRole { get; set; }
        public virtual ICollection<TmUser> InverseUserCreatedByNavigation { get; set; }
        public virtual ICollection<TmBlock> TmBlockCreatedByNavigation { get; set; }
        public virtual ICollection<TmBlock> TmBlockModifiedByNavigation { get; set; }
        public virtual ICollection<TmBlockVillage> TmBlockVillageCreatedByNavigation { get; set; }
        public virtual ICollection<TmBlockVillage> TmBlockVillageModifiedByNavigation { get; set; }
        public virtual ICollection<TmCountry> TmCountryCreatedByNavigation { get; set; }
        public virtual ICollection<TmCountry> TmCountryModifiedByNavigation { get; set; }
        public virtual ICollection<TmDistrict> TmDistrictCreatedByNavigation { get; set; }
        public virtual ICollection<TmDistrict> TmDistrictModifiedByNavigation { get; set; }
        public virtual ICollection<TmHomestay> TmHomestayApprovedByNavigation { get; set; }
        public virtual ICollection<TmHomestay> TmHomestayCreatedByNavigation { get; set; }
        public virtual ICollection<TmHomestay> TmHomestayDeactivatedByNavigation { get; set; }
        public virtual ICollection<TmHomestay> TmHomestayModifiedByNavigation { get; set; }
        public virtual ICollection<TmHomestay> TmHomestayUser { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsCreatedByNavigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsModifiedByNavigation { get; set; }
        public virtual ICollection<TmState> TmStateCreatedByNavigation { get; set; }
        public virtual ICollection<TmState> TmStateModifiedByNavigation { get; set; }
        public virtual ICollection<TmTour> TmTour { get; set; }
        public virtual ICollection<TtBankTransaction> TtBankTransaction { get; set; }
        public virtual ICollection<TtBooking> TtBooking { get; set; }
        public virtual ICollection<TtHsFeedback> TtHsFeedback { get; set; }
        public virtual ICollection<TtPackageFeedback> TtPackageFeedback { get; set; }
        public virtual ICollection<TtTourBooking> TtTourBooking { get; set; }
    }
}
