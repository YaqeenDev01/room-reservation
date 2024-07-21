using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class PermissionsLog
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public string GrantedTo { get; set; }
        public string GrantedBy { get; set; }
        public string PermissionType { get; set; }
        public DateTime DateTime { get; set; }
        public string OperationType { get; set; }
        public string? AdditionalDetails { get; set; }


    }
}
