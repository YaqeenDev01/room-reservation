using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Models;

namespace room_reservation.ViewModel
{
    public class BookingViewModel
    {
        public  int BookingId { get; set; }
        
                
        public decimal Duration { get; set; }

        public string FullName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
  
        [DisplayName("سبب الرفض")]
        public string RejectReason { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("تاريخ الحجز")]
        public DateTime BookingDate { get; set; }

        public Guid guid { get; set; } /*= Guid.NewGuid();*/

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("وقت بدء الحجز")]
        public TimeSpan BookingStart { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("وقت انتهاء الحجز")]
        public TimeSpan BookingEnd { get; set; }

        public int RoomId { get; set; }
        public bool IsDeleted { get; set; }

        public tblBookingStatues BookingStatues { get; set; }
        public int BookingStatuesId { get; set; }

        
        //adding room and room type  details for the booking details view to show them 
        public string RoomAR { get; set; }
        public int RoomNo { get; set; }
        public string BuildingNameAr { get; set; }
        public int FloorNo { get; set; }
        public int SeatCapacity { get; set; }
        public Guid RoomGuid { get; set; }



   
    }
}