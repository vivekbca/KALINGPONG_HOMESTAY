using KLMPNHomeStay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class HomestayDetailsResponseModel
    {

        public string HsId { get; set; }
        public string HomestayDescription { get; set; }
        public string LocalAttraction { get; set; }
        public string HwtReach { get; set; }
        public string AddonServices  { get; set; }
        public string HsName { get; set; }
        public string HsAddress1 { get; set; }
        public string HsAddress2 { get; set; }
        public string HsAddress3 { get; set; }
        public string HsVillId { get; set; }
        public string HsVillName { get; set; }
        public string HsBlockId { get; set; }
        public string HsBlockName { get; set; }
        public string HsDistrictId { get; set; }
        public string HsDistrictName { get; set; }
        public string HsStateId { get; set; }
        public string HsStateName { get; set; }
        public string HsCountryId { get; set; }
        public string HsCountryName { get; set; }
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
        public string HomestayImage1 { get; set; }
        public string HomestayImage2 { get; set; }
        public string HomestayImage3 { get; set; }
        public string HomestayImage4 { get; set; }
        public string HomestayImage5 { get; set; }
        public string HomestayImage6 { get; set; }
        public string HomestayImage7 { get; set; }
        public string HomestayImage8 { get; set; }
        public string HomestayImage9 { get; set; }
        public string HomestayImage10 { get; set; }
        public string HsRi1 { get; set; }
        public string HsRi2 { get; set; }
        public string HsRi3 { get; set; }
        public string HsRi4 { get; set; }
        public string HsRi5 { get; set; }
        public string HsRi6 { get; set; }
        public string HsRi7 { get; set; }
        public string HsRi8 { get; set; }
        public string HsRi9 { get; set; }
        public string HsRi10 { get; set; }
        public string VillageName { get; set; }
        public decimal? HsMapLat { get; set; }
        public decimal? HsMapLong { get; set; }
        public List<RoomForHomestayDetailsResponseModel> TmHsRooms { get; set; }
        public List<HomestayAminitiestesting> HSAmImg { get; set; }
        public List<HomestayImages> HSImages { get; set; }

    }

    public class HomestayImages
    {
        public string image { get; set; }
    }
    public class HomestayAminitiestesting
    {
        public string amin { get; set; }
        public string aminName { get; set; }
    }

}
