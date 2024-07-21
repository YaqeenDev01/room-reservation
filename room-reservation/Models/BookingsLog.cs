using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class BookingsLog
    {
        [Key]
        public int Id { get; set; }
        public string GrantedBy { get; set; }
        public int BookingId { get; set; }
        public string BookedBy { get; set; }
        public int BookingStatus { get; set; }
        public DateTime OperationDate { get; set; }
        public string? AdditionalDetails { get; set; }


    }
}
