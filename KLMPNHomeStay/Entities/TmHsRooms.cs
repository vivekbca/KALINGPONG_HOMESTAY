using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmHsRooms
    {
        public string HsId { get; set; }
        public byte HsRoomNo { get; set; }
        public string HsRoomCategoryId { get; set; }
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
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public short IsAvailable { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual TmUser CreatedByNavigation { get; set; }
        public virtual TmHomestay Hs { get; set; }
        public virtual TmHsRoomCategory HsRoomCategory { get; set; }
        public virtual TmHsFacilities HsRoomFacility10Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility11Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility12Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility13Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility14Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility15Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility1Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility2Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility3Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility4Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility5Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility6Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility7Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility8Navigation { get; set; }
        public virtual TmHsFacilities HsRoomFacility9Navigation { get; set; }
        public virtual TmUser ModifiedByNavigation { get; set; }
    }
}
