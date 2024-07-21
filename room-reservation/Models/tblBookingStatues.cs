using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblBookingStatues
    {
        public int Id { get; set; }
        public string StatuesAR { get; set; }
        public string StatuesEN { get; set; }

        public ICollection<tblBookings> Bookings { get; set; }
    }
}
