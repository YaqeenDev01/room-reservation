using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblRoomType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoomAR { get; set; }
        [Required]
        public string RoomEN { get; set; }
        [Required]
        public Guid Guid { get; set; } //
        [Required]
        public bool IsDeleted { get; set; }

        

    }
}
