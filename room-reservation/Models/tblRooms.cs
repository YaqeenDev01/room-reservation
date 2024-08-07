using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblRooms
    {
       public int Id { get; set; }   
        public int FloorId { get; set; }   
        public int RoomNo { get; set; }
        public int RoomTypeId { get; set; }
        public int SeatCapacity { get; set; }
        public bool IsActive { get; set; }
        public Guid guid { get; set; } 
        public bool IsDeleted { get; set; }
        public tblFloors Floor { get; set; }
       public tblRoomType  RoomType { get; set; }

        //public ICollection<tblFloors> FloorCollection { get; set; }


      
        public ICollection<tblFloors> Floors { get; set; }

        public ICollection<tblRoomType> RoomTypes { get; set; }

    }

}
