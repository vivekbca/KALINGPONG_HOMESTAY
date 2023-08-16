using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class BookingRequestModel
    {
        public string guId { get; set; }
        public int adult { get; set; }
        public int child { get; set; }
        public string hsId { get; set; }
        //public string userName { get; set; }
        //public string password { get; set; }
        public int totalPrice { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public List<BookDetail> bookDetail { get; set; }
    }
    public class BookDetail
    {
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public string roomNo { get; set; }
        public string categoryName { get; set; }
        public int rate { get; set; }
    }
}
