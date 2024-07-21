using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblBookingStatues
    {
        [Key]
        private int Id { get; set; }
        [Required]
        private string StatuesAR { get; set; }
        [Required]
        private string StatuesEN { get; set; }

        public ICollection<tblBookings> Bookings { get; set; }
    }
}
