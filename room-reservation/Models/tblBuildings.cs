using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblBuildings
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BuildingNo { get; set; }
        [Required]
        public string BuildingNameAr { get; set; }
        [Required]
        public string BuildingNameEn { get; set; }
        [Required]
        public int Code { get; set; } //Unique
        [Required]
        public Guid Guid { get; set; } //Unique
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public ICollection<tblFloors> Floors { get; set; }
        [Required]
        public ICollection<tblPermissions> Permissions { get; set; }
    }
}
