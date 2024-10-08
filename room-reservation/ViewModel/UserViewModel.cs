using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace room_reservation.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }

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

        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [DisplayName("الجنس")]
        public string GenderAR { get; set; }
        
        [DisplayName("Gender")]
        public string GenderEN { get; set; }

        
        [DisplayName("أسم الكلية")]
        public string? CollegeName { get; set; }
        
        [DisplayName("رمز الكلية")]
        public string? CollegeCode { get; set; }
        
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [DisplayName("أسم القسم")]
        public string DepartmentName { get; set; }
        
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [DisplayName("رمز القسم")]
        public string DepartmentCode { get; set;}


    }
}