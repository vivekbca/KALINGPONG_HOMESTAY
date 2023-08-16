using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmNotice
    {
        public string NoticeId { get; set; }
        public string Heading { get; set; }
        public string Subject { get; set; }
        public DateTime PublishingDate { get; set; }
        public string FileName { get; set; }
        public short IsDeleted { get; set; }
    }
}
