using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblFloors
    {
        [Key]
        public int Id { get; set; }
        
        public int FloorNo { get; set; }
        
        public Guid Guid { get; set; } 
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public tblBuildings Building { get; set; }
        [Required]
        public int BuildingId { get; set; }
        [Required]
        public ICollection<tblRooms> Rooms { get; set; }


    }
}
