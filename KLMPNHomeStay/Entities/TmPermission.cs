using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmPermission
    {
        public TmPermission()
        {
            TtRolePermissionMap = new HashSet<TtRolePermissionMap>();
        }

        public string PermissionId { get; set; }
        public string PermissionName { get; set; }

        public virtual ICollection<TtRolePermissionMap> TtRolePermissionMap { get; set; }
    }
}
