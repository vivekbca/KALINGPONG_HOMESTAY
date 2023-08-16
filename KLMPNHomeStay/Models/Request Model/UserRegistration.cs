using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class UserRegistration
    {

        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string UserRoleId { get; set; }
        public DateTime UserDob { get; set; }
        public string UserSex { get; set; }
        public string UserPassword { get; set; }
        
        public string UserMobileNo { get; set; }
       
        public string UserEmailId { get; set; }
        public DateTime UserLastActivity { get; set; }
        public short UserIsActive { get; set; }
        public short IsSystemDefined { get; set; }
        // public DateTime? PasswordLastChanged { get; set; }
        //  public string PreviousPasswords { get; set; }
        // public int? InvalidLoginAttempts { get; set; }
        public byte LockoutEnabled { get; set; }
        public DateTime? LockedOutUntil { get; set; }
        // public DateTime? LastLogin { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        //public DateTime UserCreatedOn { get; set; }
        //public string UserCreatedBy { get; set; }

    }
}
