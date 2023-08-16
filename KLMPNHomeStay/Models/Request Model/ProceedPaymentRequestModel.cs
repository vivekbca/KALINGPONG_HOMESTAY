using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class ProceedPaymentRequestModel
    {
        public string tourId { get; set; }
        public string tourDateId { get; set; }
        public string guId { get; set; }
        public int totalCost { get; set; }
        public int person { get; set; }
        public List<GuestDetail> guestDet { get; set; }
    }
    public class GuestDetail
    {
        public string gFirstName { get; set; }
        public string gLastName { get; set; }
        public string gDOB { get; set; }
        public string gGender { get; set; }
    }
}
