using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class HsBookingPdfModel
    {
        public string GuId { get; set; }
        public string HsId { get; set; }
        public DateTime BkBookingDate { get; set; }
        public int? TotalCost { get; set; }
        public int BkNoPers { get; set; }
        public string GuName { get; set; }

        public string HsName { get; set; }
        public DateTime BkDateFrom { get; set; }
        public DateTime BkDateTo { get; set; }

        public DateTime BkCancelledDate { get; set; }

    }
}
