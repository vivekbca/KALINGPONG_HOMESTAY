using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmMarquee
    {
        public string MarqueeId { get; set; }
        public string Heading { get; set; }
        public string Desc { get; set; }
        public short IsActive { get; set; }
    }
}
