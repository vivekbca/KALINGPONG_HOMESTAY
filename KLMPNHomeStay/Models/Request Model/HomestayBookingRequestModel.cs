using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class HomestayBookingRequestModel
    {
        public string guId { get; set; }
        public string hsId { get; set; }
        public string hsBookingId { get; set; }
        public DateTime bkBookingDate { get; set; }
        public DateTime bkDateFrom { get; set; }
        public DateTime bkDateTo { get; set; }
        public string bkNoPers { get; set; }
        public Boolean isSelected { get; set; }
        public string hsName { get; set; }
        public string guName { get; set; }
       
    }
    public class PackageBookingRequestModel
    {
        public string id { get; set; }
        public DateTime bookingDate { get; set; }
        public DateTime fromDate { get; set; }
        public string guId { get; set; }
        public string guName { get; set; }
        public string name { get; set; }
        public string noOfPerson { get; set; }
        public DateTime toDate { get; set; }
        public Boolean isSelected { get; set; }
        public string totalRate { get; set; }


    }
}
