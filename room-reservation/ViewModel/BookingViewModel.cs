using System.Collections;
using room_reservation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata;

namespace room_reservation.ViewModel
{
    public class BookingViewModel : IEnumerable
    {
        internal string FullName;

        public int Id { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("تاريخ الحجز")]
        public DateTime BookingDate { get; set; }

        public Guid guid { get; set; } /*= Guid.NewGuid();*/

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("وقت بدء الحجز")]
        public TimeSpan BookingStart { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("وقت انتهاء الحجز")]
        public TimeSpan BookingEnd { get; set; }

        //public String RejectReason { get; set; }
        //public int Dureation {  get; set; }
        //public string FullName { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        public int RoomId { get; set; }
        public bool IsDeleted { get; set; }

        public tblBookingStatues BookingStatues { get; set; }
        public int BookingStatuesId { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
    

