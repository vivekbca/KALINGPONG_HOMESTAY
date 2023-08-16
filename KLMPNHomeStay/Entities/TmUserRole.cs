using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmUserRole
    {
        public TmUserRole()
        {
            TmUser = new HashSet<TmUser>();
            TtRolePermissionMap = new HashSet<TtRolePermissionMap>();
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public short RoleIsActive { get; set; }
        public short IsSystemDefined { get; set; }
        public short IsDeleted { get; set; }

        public virtual ICollection<TmUser> TmUser { get; set; }
        public virtual ICollection<TtRolePermissionMap> TtRolePermissionMap { get; set; }
    }
}
