using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class HomeStayAddRequestModel
    {
        public string HsId { get; set; }
        public string HsName { get; set; }
        public string HsAddress1 { get; set; }
        public string HsAddress2 { get; set; }
        public string HsAddress3 { get; set; }
        public string HsVillId { get; set; }
        public string HsBlockId { get; set; }
        public string HsDistrictId { get; set; }
        public string HsStateId { get; set; }
        public string HsCountryId { get; set; }
        public string HsContactName { get; set; }
        public string HsContactMob1 { get; set; }
        public string HsContactMob2 { get; set; }
        public string HsContactEmail { get; set; }
        public int HsNoOfRooms { get; set; }
        public string HsBankName { get; set; }
        public string HsBankBranch { get; set; }
        public int HsAccountNo { get; set; }
        public string HsAccountType { get; set; }
        public string HsIfsc { get; set; }
        public string HsMicr { get; set; }
        public short IsActive { get; set; }
        public string ActiveSince { get; set; }
    }
}
