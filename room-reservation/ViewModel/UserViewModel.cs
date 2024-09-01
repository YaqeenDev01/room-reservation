using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace room_reservation.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("الأسم الكامل بالعربي")]
        public string FullNameAR { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("الأسم الكامل بالإنجليزي")]
        public string FullNameEN { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("البريد الإلكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("رقم الجوال")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("كلمة المرور")]
        public string Password { get; set; }

        [Required(ErrorMessage = " هذا الحقل مطلوب")]
        [DisplayName("نوع المستخدم")]
        public string UserType { get; set; }
        public bool IsDeleted { get; set; }


    }
}