using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class PackageDateListResponseModel
    {
        public string dateId { get; set; }
        public string tourId { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public short isActive { get; set; }
    }
}
