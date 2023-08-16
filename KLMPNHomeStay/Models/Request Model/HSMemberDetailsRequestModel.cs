using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class HSMemberDetailsRequestModel
    {
        public string hsId { get; set; }
        public string hsName { get; set; }
        public string roomCategory { get; set; }
        public object roomFacility { get; set; }
        public int roomNo { get; set; }
        public int roomRate { get; set; }
        public string roomFloor { get; set; }
        public string noOfBeds { get; set; }
        public string roomSize { get; set; }
        public string isAvailable { get; set; }
        public string hsImage1 { get; set; }
        public string hsImage2 { get; set; }
        public string hsImage3 { get; set; }
        public string hsImage4 { get; set; }
        public string hsImage5 { get; set; }
        public string hsImage6 { get; set; }
        public string hsImage7 { get; set; }
        public string hsImage8 { get; set; }
        public string hsImage9 { get; set; }
        public string hsImage10 { get; set; }
        public string roomImage1 { get; set; }
        public string roomImage2 { get; set; }
        public string roomImage3 { get; set; }
        public string roomImage4 { get; set; }
        public string roomImage5 { get; set; }
        public string roomImage6 { get; set; }
        public string roomImage7 { get; set; }
        public string roomImage8 { get; set; }
        public string roomImage9 { get; set; }
        public string roomImage10 { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public string facility1 { get; set; }
        public string facility2 { get; set; }
        public string facility3 { get; set; }
        public string facility4 { get; set; }
        public string facility5 { get; set; }
        public string facility6 { get; set; }
        public string facility7 { get; set; }
        public string facility8 { get; set; }
        public string facility9 { get; set; }
        public string facility10 { get; set; }
        public string facility11 { get; set; }
        public string facility12 { get; set; }
        public string facility13 { get; set; }
        public string facility14 { get; set; }
        public string facility15 { get; set; }
        public object roomDetailsList { get; set; }
        public string roomImageStrings { get; set; }
    }
    public class RoomFacility
    {
        public string hsFacilityId { get; set; }
        public string hsFacilityName { get; set; }
    }
    public class RoomDetails
    {
        public string  roomCategory { get; set; }
        public object roomFacility { get; set; }
        public string roomNo { get; set; }
        public string roomRate { get; set; }
        public string roomFloor { get; set; }
        public string noOfBeds { get; set; }
        public string roomSize { get; set; }
        public string isAvailable { get; set; }
        public string roomImage { get; set; }
    }
}
