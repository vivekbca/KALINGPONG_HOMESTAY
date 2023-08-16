using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class TourFeedbackAddRequestModel
    {
       public string tourBookingId { get; set; }
       public string tourFeedback { get; set; }
       public int tourRatings{ get; set; }
}
}
