using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class tblBuildings
    {
        public int Id { get; set; }
        public int BuildingNo { get; set; }
        public string BuildingNameAr { get; set; }
        public string BuildingNameEn { get; set; }
        public string Code { get; set; } //Unique
        public Guid Guid { get; set; } //Unique
        public bool IsDeleted { get; set; }
        public ICollection<tblFloors> Floors { get; set; }
        public ICollection<tblPermissions> Permissions { get; set; }

        public tblGender Gender { get; set; }
        public int GenderId { get; set; }
    }
}
