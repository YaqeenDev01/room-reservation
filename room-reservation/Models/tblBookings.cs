using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblBookings
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        //[Unique]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateOnly BookingDate { get; set; }
        [Required]
        public TimeOnly BookingStart { get; }
        [Required]
        public TimeOnly BookingEnd { get; }
        [Required]
        public int Duration { get; }
        [Required]
        //[Unique]
        public Guid guid { get; set; }

        public string? RejectReason { get; set; }

        //change
        public  tblBookingStatues BookingStatues { get; set; }
        public int BookingStatuesId { get; set; }
        


    }
}

