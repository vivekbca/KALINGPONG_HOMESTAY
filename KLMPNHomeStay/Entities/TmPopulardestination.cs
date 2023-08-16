using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmPopulardestination
    {
        public TmPopulardestination()
        {
            TmHomestay = new HashSet<TmHomestay>();
        }

        public string DestinationId { get; set; }
        public string DestinationName { get; set; }
        public string PopulardestinationImage { get; set; }
        public short IsActive { get; set; }

        public virtual ICollection<TmHomestay> TmHomestay { get; set; }
    }
}
