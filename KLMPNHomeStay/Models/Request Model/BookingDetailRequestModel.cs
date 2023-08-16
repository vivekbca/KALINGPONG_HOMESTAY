using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class BookingDetailRequestModel
    {
        public string hsId { get; set; }
        public string adultNo { get; set; }
        public string childNo { get; set; }
        public int totalRate { get; set; }
        public int discountRate { get; set; }
        public string discountType { get; set; }
        public List<BookingRoomDetail> bookingRoomDetails { get; set; }
    }
    public class BookingRoomDetail
    {
        //public string date { get; set; }
        //public int roomNo { get; set; }
        public int roomNo { get; set; }
        //public string bookedDate { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public int rate { get; set; }
    }
}
