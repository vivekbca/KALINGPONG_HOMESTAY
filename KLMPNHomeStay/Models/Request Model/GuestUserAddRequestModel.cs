using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class GuestUserAddRequestModel
    {
        public string GuId { get; set; }
        public string GuName { get; set; }
        public string GuAddress1 { get; set; }
        public string GuAddress2 { get; set; }
        public string GuAddress3 { get; set; }
        public string GuState { get; set; }
        public string GuCountry { get; set; }
        public string GuDob { get; set; }
        public string GuSex { get; set; }
        public string GuPassword { get; set; }
        public string GuPincode { get; set; }
        public string GuIdentityProof { get; set; }
        public string GuIdentityNo { get; set; }
        public string GuCity { get; set; }

        [Required(ErrorMessage = "Please Enter Contact Number")]
        [StringLength(10, ErrorMessage = "Mobile Number cannot be longer than 10 digit.")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid phone number")]
        public string GuMobileNo { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [StringLength(maximumLength: 100, ErrorMessage = "Please enter Email within 100 characters")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        public string GuEmailId { get; set; }
        public string GuCreatedOn { get; set; }
        public string GuLastActivity { get; set; }
        public short GuIsActive { get; set; }

        public string repassword { get; set; }
    }
}
