using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblFloors
    {
       public int Id { get; set; }
        
        public int FloorNo { get; set; }
        
        public Guid Guid { get; set; } 
       
        public bool IsDeleted { get; set; }
        public tblBuildings Building { get; set; }
        public int BuildingId { get; set; }
        public ICollection<tblRooms> Rooms { get; set; }


    }
}
