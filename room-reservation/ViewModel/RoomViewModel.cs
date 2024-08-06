using room_reservation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

    namespace room_reservation.ViewModel
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [DisplayName("رقم القاعة")]
        public int RoomNo { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("عدد المقاعد")]
        public int SeatCapacity { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("حالة القاعة")]
        public bool IsActive { get; set; }

        public Guid Guid { get; set; } 
        public tblFloors Floor { get; set; }

       // [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("رقم الدور")]
        public int FloorId { get; set; }
     


        public tblRoomType RoomType { get; set; }

        //[Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("نوع القاعة")]
        public int RoomTypeId { get; set; }        
        public bool IsDeleted { get; set; }
        }
    }



