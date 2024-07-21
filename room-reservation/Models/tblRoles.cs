using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{//manually 
    public class tblRoles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoleNameEN { get; set; }
        [Required]
        public string RoleNameAR { get; set; }
        [Required]
        public Guid guid { get; set; } 
        [Required]
        public bool IsDeleted { get; set; }
        [Required]

     
        public ICollection<tblPermissions> Permissions { get; set; }

    }
}
