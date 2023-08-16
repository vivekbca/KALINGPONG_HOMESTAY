using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmVillageCategory
    {
        public TmVillageCategory()
        {
            TmHomestay = new HashSet<TmHomestay>();
        }

        public string VillageCategoryId { get; set; }
        public string VillageCategoryName { get; set; }

        public virtual ICollection<TmHomestay> TmHomestay { get; set; }
    }
}
