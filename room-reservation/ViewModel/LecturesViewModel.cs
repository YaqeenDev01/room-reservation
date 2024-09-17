using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace room_reservation.ViewModel
{
    public class LecturesViewModel
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName(" رقم المبنى")]
        [Range(1, 9999, ErrorMessage = "رقم المبنى لايمكن ان يكون سالبًا ")]
        public string BuildingNo { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("رقم القاعة")]
        [Range(1, 9999, ErrorMessage = "رقم القاعة لايمكن ان يكون سالبًا")]
        public string RoomNo { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("الفصل الدراسي")]
        public string Semester { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("وقت بدء المحاضرة")]

        public TimeSpan StartLectureTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("وقت انتهاء المحاضرة")]
        public TimeSpan EndLectureTime { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("تاريخ المحاضرة")]
        public DateTime LectureDate { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("مدة المحاضرة")]
        public decimal LectureDurations { get; set; }
    }
}


