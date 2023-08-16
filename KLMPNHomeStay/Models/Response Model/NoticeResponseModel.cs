using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class NoticeResponseModel
    {
        public string noticeId { get; set; }
        public string heading { get; set; }
        public string subject { get; set; }
        public string publishingDate { get; set; }
        public string fileName { get; set; }
        public short isDeleted { get; set; }
    }
}
