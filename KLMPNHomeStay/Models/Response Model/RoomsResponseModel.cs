using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class RoomsResponseModel
    {
        public string HsId { get; set; }
        public byte HsRoomNo { get; set; }
        public string HsRoomCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int HsRoomRate { get; set; }
        public string HsRoomFloor { get; set; }
        public byte HsRoomNoBeds { get; set; }
        public string HsRoomSize { get; set; }
        public string HsRoomImage { get; set; }
        public string HsRoomFacility1 { get; set; }
        public string HsRoomFacility2 { get; set; }
        public string HsRoomFacility3 { get; set; }
        public string HsRoomFacility4 { get; set; }
        public string HsRoomFacility5 { get; set; }
        public string HsRoomFacility6 { get; set; }
        public string HsRoomFacility7 { get; set; }
        public string HsRoomFacility8 { get; set; }
        public string HsRoomFacility9 { get; set; }
        public string HsRoomFacility10 { get; set; }
        public string HsRoomFacility11 { get; set; }
        public string HsRoomFacility12 { get; set; }
        public string HsRoomFacility13 { get; set; }
        public string HsRoomFacility14 { get; set; }
        public string HsRoomFacility15 { get; set; }
        public short HsRoomAvailable { get; set; }
        public short IsAvailable { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    
    public class IdDtls
    {
       public string hsId { get; set; }
       public string roomNo { get; set; }
    }
}
