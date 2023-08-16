using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class Package
    {
        public string PName { get; set; }
        public string DName { get; set; }
        public string Day { get; set; }
        public string Night { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobile { get; set; }
        public string FacilityId1 { get; set; }
        public string FacilityId2 { get; set; }
        public string FacilityId3 { get; set; }
        public string FacilityId4 { get; set; }
        public string FacilityId5 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string PackagePdf { get; set;}
        public string CreatedBy { get; set; }
        public List<RoomFacilityy> roomFacility { get; set; }
    }
    public class RoomFacilityy
    {
        public string hsFacilityId { get; set; }
        public string hsFacilityName { get; set; }
    }
}
