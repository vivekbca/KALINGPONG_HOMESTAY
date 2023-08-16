using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class HSApproveRjectRequestModel
    {
        public string userId { get; set; }
        public string hsId { get; set; }
        public string approvalType { get; set; }
    }
}
