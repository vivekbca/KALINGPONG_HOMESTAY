using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class GuestDetailResponseModel
    {
        public string guId { get; set; }
        public string guFirstName { get; set; }
        public string guLastName { get; set; }
        public string address { get; set; }
        public string stateId { get; set; }
        public string countryId { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string emailId { get; set; }
        public string mobileNo { get; set; }
        public string stateName { get; set; }
        public string countryName { get; set; }
        public string pinCode { get; set; }
        public string city { get; set; }
        public string identityType { get; set; }
        public string identityNo { get; set; }
    }
}
