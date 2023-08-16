using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtRolePermissionMap
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string PermissionId { get; set; }

        public virtual TmPermission Permission { get; set; }
        public virtual TmUserRole Role { get; set; }
    }
}
