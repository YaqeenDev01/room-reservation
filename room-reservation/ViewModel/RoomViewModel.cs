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

        //[Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("حالة الغرفة")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("رقم الدور")]
        public int FloorId { get; set; }

        public tblFloors Floor { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("نوع الغرفة")]
        public int RoomTypeId { get; set; }
        public  tblRoomType RoomType;
        public string RoomAR;
        public ICollection<tblFloors> FloorCollection { get; set; }

        public ICollection<tblRoomType> RoomTypeCollection { get; set; }
        
        
        //adding building details for the floor index view to show them 
        
        public int BuildingNo { get; set; }
        public string BuildingNameAr { get; set; }
        public string BuildingNameEn { get; set; }
        
        
  
    }
    }

