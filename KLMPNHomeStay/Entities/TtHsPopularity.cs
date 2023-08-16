using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtHsPopularity
    {
        public string HsId { get; set; }
        public int HsSearchCount { get; set; }

        public virtual TmHomestay Hs { get; set; }
    }
}
