using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class TenderAddRequestModel
    {
        public string tenderId { get; set; }
        public string subject { get; set; }
        public string finYrId { get; set; }
        public string memoNo { get; set; }
        public string closingDate { get; set; }
        public IFormFile tenderFile { get; set; }
    }
}
