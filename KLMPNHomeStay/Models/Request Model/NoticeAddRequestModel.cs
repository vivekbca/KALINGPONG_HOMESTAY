using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class NoticeAddRequestModel
    {
        public string noticeId { get; set; }
        public string heading { get; set; }
        public string subject { get; set; }
        public IFormFile noticeFile { get; set; }
    }
}
