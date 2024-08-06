using room_reservation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace room_reservation.ViewModel
{
    public class RoomViewModel
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("رقم الغرفة")]
        public int RoomNo { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("عدد المقاعد")]
        public int SeatCapacity { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("حالة الغرفة")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("رقم الدور")]
        public int FloorId { get; set; }

        public tblFloors Floor { get; set; }
        public int FloorNo { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("نوع الغرفة")]
        public int RoomTypeId { get; set; }

        public tblRoomType RoomAR { get; set; }
        public string RoomTypeName { get; set; }

        public List<SelectListItem> Floors { get; set; }
        public List<SelectListItem> RoomTypes { get; set; }
    }
}
