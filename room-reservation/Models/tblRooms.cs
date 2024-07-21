using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblRooms
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoomNo { get; set; }
        [Required]
        public int SeatCapacity { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public Guid Guid { get; set; } //
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public tblFloors Floor { get; set; }
        [Required]
        public int FloorId { get; set; }
        [Required]

        public tblRoomType  RoomType { get; set; }

        public int RoomTypeId { get; set; }
    }
}
