using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblLectures
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BuildingNo { get; set; }
        [Required]
        public string RoomNo { get; set; }
        [Required]
        public string Semester { get; set; }
        [Required]
        public TimeOnly LectureTime { get; set; }
        [Required]
        public DateOnly LectureDate { get; set; }
        [Required]
        public TimeOnly LectureDuration { get; set; }
    }
}
