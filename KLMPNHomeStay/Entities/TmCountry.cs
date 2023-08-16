using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmCountry
    {
        public TmCountry()
        {
            TmBlock = new HashSet<TmBlock>();
            TmBlockVillage = new HashSet<TmBlockVillage>();
            TmDistrict = new HashSet<TmDistrict>();
            TmGuestUser = new HashSet<TmGuestUser>();
            TmHomestay = new HashSet<TmHomestay>();
            TmState = new HashSet<TmState>();
        }

        public string CountryId { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; }
        public short IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual TmUser CreatedByNavigation { get; set; }
        public virtual TmUser ModifiedByNavigation { get; set; }
        public virtual ICollection<TmBlock> TmBlock { get; set; }
        public virtual ICollection<TmBlockVillage> TmBlockVillage { get; set; }
        public virtual ICollection<TmDistrict> TmDistrict { get; set; }
        public virtual ICollection<TmGuestUser> TmGuestUser { get; set; }
        public virtual ICollection<TmHomestay> TmHomestay { get; set; }
        public virtual ICollection<TmState> TmState { get; set; }
    }
}
