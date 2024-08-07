using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblLectures
    {

        public int Id { get; set; }

        public string BuildingNo { get; set; }

        public string RoomNo { get; set; }

        public string Semester { get; set; }

        public TimeSpan StartLectureTime { get; set; }

        public TimeSpan EndLectureTime { get; set; }
        public DateTime LectureDate { get; set; }

        public decimal LectureDurations { get; set; }
    }
}
