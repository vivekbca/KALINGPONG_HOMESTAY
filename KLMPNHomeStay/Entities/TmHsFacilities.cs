using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmHsFacilities
    {
        public TmHsFacilities()
        {
            TmHsRoomsHsRoomFacility10Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility11Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility12Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility13Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility14Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility15Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility1Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility2Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility3Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility4Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility5Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility6Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility7Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility8Navigation = new HashSet<TmHsRooms>();
            TmHsRoomsHsRoomFacility9Navigation = new HashSet<TmHsRooms>();
            TmTourFacilityId1Navigation = new HashSet<TmTour>();
            TmTourFacilityId2Navigation = new HashSet<TmTour>();
            TmTourFacilityId3Navigation = new HashSet<TmTour>();
            TmTourFacilityId4Navigation = new HashSet<TmTour>();
            TmTourFacilityId5Navigation = new HashSet<TmTour>();
        }

        public string HsFacilityId { get; set; }
        public string HsFacilityName { get; set; }
        public string FileName { get; set; }

        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility10Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility11Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility12Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility13Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility14Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility15Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility1Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility2Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility3Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility4Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility5Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility6Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility7Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility8Navigation { get; set; }
        public virtual ICollection<TmHsRooms> TmHsRoomsHsRoomFacility9Navigation { get; set; }
        public virtual ICollection<TmTour> TmTourFacilityId1Navigation { get; set; }
        public virtual ICollection<TmTour> TmTourFacilityId2Navigation { get; set; }
        public virtual ICollection<TmTour> TmTourFacilityId3Navigation { get; set; }
        public virtual ICollection<TmTour> TmTourFacilityId4Navigation { get; set; }
        public virtual ICollection<TmTour> TmTourFacilityId5Navigation { get; set; }
    }
}
