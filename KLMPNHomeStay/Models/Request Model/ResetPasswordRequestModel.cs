using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class ResetPasswordRequestModel
    {
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string token { get; set; }
    }
}
