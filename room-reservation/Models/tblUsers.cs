using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblUsers
    {
        public int Id { get; set; }
        public string FullNameAR { get; set; }
        public string FullNameEN { get; set; }
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsDeleted { get; set; }
        public string genderAR { get; set; } //أنثى ذكر dropdown list
        public string genderEN { get; set; } //Female Male dropdown list


    }
}
