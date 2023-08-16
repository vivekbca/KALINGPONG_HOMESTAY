using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class TMBlockRequestModel
    {
        public string BlockId { get; set; }
        public int BlockCode { get; set; }
        public string BlockName { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string DistrictId { get; set; }
        public short IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
    }
}
