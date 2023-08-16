﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class PackagRefundListResponseModel
    {
        public string bookingId { get; set; }
        public string tourId { get; set; }
        public string bookingDate { get; set; }
        public string cancelDate { get; set; }
        public string tourName { get; set; }
        public string tourDtId { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public int person { get; set; }
        public int? paymentAmount { get; set; }
        public string destination { get; set; }
    }
}
