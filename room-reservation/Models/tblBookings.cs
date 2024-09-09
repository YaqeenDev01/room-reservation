using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblBookings
    {
       
        public int Id { get; set; }
        
        public string FullName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public DateTime BookingDate { get; set; }
        
        public TimeSpan BookingStart { get; set; }
        
        public TimeSpan BookingEnd { get; set; }
       
        public decimal Duration { get; set; }
        
        public Guid guid { get; set; }

        public string? RejectReason { get; set; }
     
        public  bool IsDeleted { get; set; }

        public tblRooms Room { get; set;}
        public int RoomId { get; set; }

        public  tblBookingStatues BookingStatues { get; set; }
        public int BookingStatuesId { get; set; }

        public tblGender Gender { get; set; }
        public int GenderId { get; set;}
        
    }
}

