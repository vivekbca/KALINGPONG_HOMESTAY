using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmBlock
    {
        public TmBlock()
        {
            TmBlockVillage = new HashSet<TmBlockVillage>();
            TmHomestay = new HashSet<TmHomestay>();
        }

        public string BlockId { get; set; }
        public int BlockCode { get; set; }
        public string BlockName { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string DistrictId { get; set; }
        public short IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual TmCountry Country { get; set; }
        public virtual TmUser CreatedByNavigation { get; set; }
        public virtual TmDistrict District { get; set; }
        public virtual TmUser ModifiedByNavigation { get; set; }
        public virtual TmState State { get; set; }
        public virtual ICollection<TmBlockVillage> TmBlockVillage { get; set; }
        public virtual ICollection<TmHomestay> TmHomestay { get; set; }
    }
}
