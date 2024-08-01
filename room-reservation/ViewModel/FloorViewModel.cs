using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using room_reservation.Models;

namespace room_reservation.ViewModel
{
    public class FloorViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("رقم الطابق")]
        public int FloorNo { get; set; }

        public Guid Guid { get; set; } = Guid.NewGuid();

        public tblBuildings Building { get; set; }
        public int BuildingId { get; set; }
        public ICollection<tblRooms> Rooms { get; set; }
    }
}