using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class RoomAvailabilityModel
    {
        public string HsId { get; set; }
        public byte HsRoomNo { get; set; }
        public string HsRoomCategoryId { get; set; }
        public string HsRoomCategoryName { get; set; }
        public int HsRoomRate { get; set; }
        public bool isChecked { get; set; }
        public short HsRoomAvailable { get; set; }
        public short isAvailable { get; set; }
    }

    public class CheckAvailabilityResponseModel
    {
        //public string hsId { get; set; }
        //public string hsName { get; set; }
        //public string fromDt { get; set; }
        //public string toDt { get; set; }
        //public int noOfRooms { get; set; }
        public string date { get; set; }
        public List<RoomAvailabilityModel> roomAvailabilityModels { get; set; }
    }
}
