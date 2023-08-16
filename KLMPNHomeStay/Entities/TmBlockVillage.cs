using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmBlockVillage
    {
        public TmBlockVillage()
        {
            TmHomestay = new HashSet<TmHomestay>();
        }

        public string VillId { get; set; }
        public int VillCode { get; set; }
        public string VillName { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string DistrictId { get; set; }
        public string BlockId { get; set; }
        public string VillImage { get; set; }
        public short IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual TmBlock Block { get; set; }
        public virtual TmCountry Country { get; set; }
        public virtual TmUser CreatedByNavigation { get; set; }
        public virtual TmDistrict District { get; set; }
        public virtual TmUser ModifiedByNavigation { get; set; }
        public virtual TmState State { get; set; }
        public virtual ICollection<TmHomestay> TmHomestay { get; set; }
    }
}
