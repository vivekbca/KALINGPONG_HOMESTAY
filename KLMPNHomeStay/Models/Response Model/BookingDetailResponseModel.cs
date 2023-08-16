using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class BookingDetailResponseModel
    {
        public string hsId { get; set; }
        public string adultNo { get; set; }
        public string childNo { get; set; }
        public int totalRate { get; set; }
        public int discountRate { get; set; }
        public string discountType { get; set; }
        public List<BookingResponse> bookingResponses { get; set; }
    }
    public class BookingResponse
    {
        public int roomNo { get; set; }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
    }
}
