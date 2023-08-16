using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class FeedBackListResponseModel
    {
        public string GuId { get; set; }
        public string HsId { get; set; }
        public string HsBookingId { get; set; }
        public DateTime BkDateFrom { get; set; }
        public DateTime BkDateTo { get; set; }
        public string HsName { get; set; }
        public string GuName { get; set; }
        public string HsAddress1 { get; set; }
        public Boolean IsFeedBackView { get; set; }

    }
}
