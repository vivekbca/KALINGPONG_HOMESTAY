using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class DistrictAddRequestModel
    {
        public string DistrictId { get; set; }
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public short IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
    }
}
