using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class SearchResponseModel
    {
        public string HS_Id { get; set; }
        public string HS_Name { get; set; }
        public string HS_Address1 { get; set; }
        public string HS_Address2 { get; set; }
        public string HS_Address3 { get; set; }
        public string HS_VillId { get; set; }
        public string HS_VillName { get; set; }
        public string HS_BlockId { get; set; }
        public string HS_BlockName { get; set; }
        public string HS_DistId { get; set; }
        public string HS_DistName { get; set; }
        public string HS_StateId { get; set; }
        public string HS_StateName { get; set; }
        public string HS_ContactName { get; set; }
        public string HS_ContactMob1 { get; set; }
        public string HS_ContactMob2 { get; set; }
        public string HS_ContactEmail { get; set; }
        public int HS_NoOfRooms { get; set; }
        public DateTime ActiveSince { get; set; }
        public int RoomRate { get; set; }
    }
}
