using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmDistrict
    {
        public TmDistrict()
        {
            TmBlock = new HashSet<TmBlock>();
            TmBlockVillage = new HashSet<TmBlockVillage>();
            TmHomestay = new HashSet<TmHomestay>();
        }

        public string DistrictId { get; set; }
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public short IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual TmCountry Country { get; set; }
        public virtual TmUser CreatedByNavigation { get; set; }
        public virtual TmUser ModifiedByNavigation { get; set; }
        public virtual TmState State { get; set; }
        public virtual ICollection<TmBlock> TmBlock { get; set; }
        public virtual ICollection<TmBlockVillage> TmBlockVillage { get; set; }
        public virtual ICollection<TmHomestay> TmHomestay { get; set; }
    }
}
