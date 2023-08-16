using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class CheckAvailabilityRequestModel
    {
        public string hsId { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public int noOfRooms { get; set; }
    }
}
