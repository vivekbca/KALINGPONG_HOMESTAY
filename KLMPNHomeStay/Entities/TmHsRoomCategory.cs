using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmHsRoomCategory
    {
        public TmHsRoomCategory()
        {
            TmHsRooms = new HashSet<TmHsRooms>();
        }

        public string HsCategoryId { get; set; }
        public string HsCategoryName { get; set; }

        public virtual ICollection<TmHsRooms> TmHsRooms { get; set; }
    }
}
