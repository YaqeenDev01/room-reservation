using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblPermissions 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email  { get; set;}
        [Required]
        public Guid guid { get; set; } = Guid.NewGuid();
        
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
       public tblRoles Role { get; set; }
        [Required]
       public int RoleId { get; set; }
        [Required]
       public tblBuildings Building { get; set; }

       public int? BuildingId { get; set; }





    }
}
