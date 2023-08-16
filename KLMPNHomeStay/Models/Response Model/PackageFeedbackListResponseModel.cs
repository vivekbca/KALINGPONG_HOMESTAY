using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class PackageFeedbackListResponseModel
    {
        public string GuId { get; set; }
        public string TourId { get; set; }
        public string Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Name { get; set; }
        public string GuName { get; set; }
        public string Destination { get; set; }
        public Boolean IsFeedBackView { get; set; }
        public Boolean IsGiveFeedBack { get; set; }
    }
}
