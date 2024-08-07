using room_reservation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace room_reservation.ViewModel
{
    public class BuildingViewModel
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("رقم المبنى ")]
        public int BuildingNo { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("الاسم")]
        public string BuildingNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("Name ")]
        public string BuildingNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("كود المبنى")]
        public int Code { get; set; } //Unique
        public Guid Guid { get; set; }  //Unique

        //public ICollection<tblPermissions> Permissions { get; set; }
    
        //public ICollection<tblFloors> Floors { get; set; }

    }
}


