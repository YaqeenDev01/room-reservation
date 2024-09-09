using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblPermissions 
    {
        public int Id { get; set; }
        public string Email  { get; set;}
        public Guid guid { get; set; } = Guid.NewGuid();
        
        public bool IsDeleted { get; set; }
       public tblRoles Role { get; set; }
       public int RoleId { get; set; }
       public tblBuildings Building { get; set; }

       public int? BuildingId { get; set; }



    }
}
