using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class HomeStayFeedbackModel
    {
        public string HomestayName { get; set; } 
        public string Address { get; set; }
        public string Amonunt { get; set; }
        public string RoomBooked { get; set; }
        public string bookedfrom { get; set; }
        public string bookedTo { get; set; }
        public string Guid { get; set; }
        public string BookingId { get; set; }
        public string feedbackid { get; set; }
        public short isCanceled { get; set; }
        public short isCompleted { get; set; }
        public bool isUpcoming { get; set; }
        public bool isFeedbackGiven { get; set; }
        public string status { get; set; }
        
    }
}
