using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class MarqueeResponseModel
    {
        public string marqueeId { get; set; }
        public string heading { get; set; }
        public string desc { get; set; }
        public short isActive { get; set; }
    }
}
