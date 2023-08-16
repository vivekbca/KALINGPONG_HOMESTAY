using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class BookedRoomsResponseModel
    {
        public string hsId { get; set; }
        public string hsName { get; set; }
        public float totalRate { get; set; }
        public int totalRooms { get; set; }
        public int totalNights { get; set; }
        public List<BookedRooms> bookedRoomsModels { get; set; }
    }
    public class BookedRooms
    {
        public int roomNo { get; set; }
        //public string bookedDate { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public int rate { get; set; }

    }
}
