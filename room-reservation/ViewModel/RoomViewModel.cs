using room_reservation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace room_reservation.ViewModel
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("رقم الغرفة")]
        public int RoomNo { get; set; }

        public int SeatCapacity { get; set; }

        public bool IsActive { get; set; }

        public int FloorId { get; set; }
        public Guid FloorGuid { get; set; }
        public tblFloors Floor { get; set; }
        public int FloorNo { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("نوع الغرفة")]
        public int RoomTypeId { get; set; }
        public  tblRoomType RoomType { get; set; }
        public IEnumerable<SelectListItem> RoomTypes { get; set; }
        public string RoomAR { get; set; }
        
        //adding building details for the floor index view to show them 

        [DisplayName("اسم المبنى")]
        public string BuildingNameAr { get; set; }
        
        public string BuildingNameEn { get; set; }
        public tblBuildings Building { get; set; }
        public Guid BuildingGuid { get; set; }

  
    }
    }

