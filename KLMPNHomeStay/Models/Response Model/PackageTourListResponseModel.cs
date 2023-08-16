using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class PackageTourListResponseModel
    {
        public string tourId { get; set; }
        public string tourName { get; set; }
        public string destination { get; set; }
        public int destinationDay { get; set; }
        public int destinationNight { get; set; }
        public int cost { get; set; }
        public string contactPersonName { get; set; }
        public int? contactPersonMobile { get; set; }
        public string contactPersonEmail { get; set; }
        public string img { get; set; }
    }
}
