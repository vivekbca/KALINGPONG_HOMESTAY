using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class TenderResponseModel
    {
        public string tenderId { get; set; }
        public string subject { get; set; }
        public string finYrId { get; set; }
        public string finYr { get; set; }
        public string publishingDate { get; set; }
        public string memoNo { get; set; }
        public short isPublished { get; set; }
        public string closingDate { get; set; }
        public string fileName { get; set; }
    }
}
