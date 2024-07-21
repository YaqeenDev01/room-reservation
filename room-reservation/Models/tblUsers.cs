using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblUsers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullNameAR { get; set; }
        [Required]
        public string FullNameEN { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserType { get; set; }
      
        [Required]
        public bool IsDeleted { get; set; }
    }
}
