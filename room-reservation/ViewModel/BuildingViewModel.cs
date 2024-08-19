using room_reservation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace room_reservation.ViewModel
{
    public class BuildingViewModel
    {
        public int BuildingId { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("رقم المبنى ")]
        [Range(0, int.MaxValue, ErrorMessage = " هذا الحقل لا يقبل القيم السالبة ")]

        public int BuildingNo { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("الاسم")]
        public string BuildingNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("Name ")]
        public string BuildingNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayName("كود المبنى")]
        [Range(0, int.MaxValue,ErrorMessage =" هذا الحقل لا يقبل القيم السالبة ") ]
        public int Code { get; set; } //Unique
        public Guid Guid { get; set; }  //Unique

        //public ICollection<tblPermissions> Permissions { get; set; }
    
        //public ICollection<tblFloors> Floors { get; set; }

    }
}


