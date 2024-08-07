using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Models;
using System.ComponentModel.DataAnnotations;

namespace room_reservation.ViewModel
{
    //In view models, we will include required and display name. 
    //Remove those that will not be displayed to the users. For instance, isDeleted is removed.
    //View model controls what input will be entered and their constraints.
    public class PermissionViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني المدخل غير صحيح")]
        public string Email { get; set; }
        public Guid guid { get; set; } = Guid.NewGuid();

        //Ask Eng .... fk 
        public tblRoles Role { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public tblBuildings Building { get; set; }
        public int? BuildingId { get; set; }
        
        public string BuildingName { get; set; }
        public int BuildingNum { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
        public IEnumerable<SelectListItem> Buildings { get; set; }



    }


}
