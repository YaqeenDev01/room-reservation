using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using room_reservation.Models;

namespace room_reservation.ViewModel
{
    public class FloorViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("رقم الطابق")]
        [Range(1,100,ErrorMessage = "رقم الطابق لا يمكن أن يكون سالبًا")]
        public int FloorNo {get; set;}
        public Guid Guid {get; set;} = Guid.NewGuid();
        
        public bool IsDeleted { get; set; }
        
        
        //adding building details for the floor index view to show them 

        public string BuildingNameAr { get; set; }
        
      
        public int BuildingNo { get; set; }
    

        public tblBuildings Building { get; set; }
        public int BuildingId { get; set; }
        
        public ICollection<tblRooms> Rooms { get; set; }
    }
}