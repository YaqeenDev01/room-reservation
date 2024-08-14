﻿using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblRoomType
    {
        public int Id { get; set; }
        public string RoomAR { get; set; }
        public string RoomEN { get; set; }
        public Guid guid { get; set; } 
        public bool IsDeleted { get; set; }

         public ICollection<tblRooms> RoomsCollection { get; set; }
    }
}
