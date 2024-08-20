using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using room_reservation.Models;

namespace room_reservation.ViewModel
{
    public class FloorViewModel
    {

        //public int Id { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("رقم الطابق")]
        [Range(1,9999,ErrorMessage = "رقم الطابق لابد ان يكون مابين ١ و ٩٩٩٩")]
        public int FloorNo { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        
        public bool IsDeleted { get; set; }
        
        
        //adding building details for the floor index view to show them 
        public string BuildingNameAr { get; set; }
        public int BuildingNo { get; set; }
    

        public tblBuildings Building { get; set; }
        public int BuildingId { get; set; }
        
        public ICollection<tblRooms> Rooms { get; set; }
    }
}