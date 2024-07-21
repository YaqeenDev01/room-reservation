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
        public TimeSpan LectureTime { get; set; }
        [Required]
        public DateTime LectureDate { get; set; }
        [Required]
        public TimeSpan LectureDuration { get; set; }
    }
}
