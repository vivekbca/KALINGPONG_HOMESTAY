using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class TourFeedbackViewResponseModel
    {
        public string tourId { get; set; }
        public string tourName { get; set; }
        public string destination { get; set; }
        public string tourDateId { get; set; }
        public string fromDt { get; set; }
        public string toDt { get; set; }
        public string feedbackId { get; set; }
        public string feedback { get; set; }
        public int? feedbackRating { get; set; }
    }
}
