using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class UserRoleResponseModel
    {
        public string roleId { get; set; }
        public string roleName { get; set; }
        public short roleIsActive { get; set; }
        public short isSystemDefined { get; set; }
        public short isDeleted { get; set; }
    }
}
