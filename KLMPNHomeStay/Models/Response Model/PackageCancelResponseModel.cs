using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class PackageCancelResponseModel
    {
       public string tourName { get; set; }
       public string destination { get; set; }
       public string tourId { get; set; }
       public string tourBookingId { get; set; }
       public string tourDateId { get; set; }
       public string fromDt { get; set; }
       public string toDt { get; set; }
       public int? totalAmount { get; set; }
    }
}
