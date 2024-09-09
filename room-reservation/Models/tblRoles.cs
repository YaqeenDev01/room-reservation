using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{//manually 
    public class tblRoles
    {
        public int Id { get; set; }
        public string RoleNameAR { get; set; }
        public string RoleNameEN { get; set; }

        public Guid guid { get; set; } 
        public bool IsDeleted { get; set; }
        public ICollection<tblPermissions> Permissions { get; set; }

    }
}
