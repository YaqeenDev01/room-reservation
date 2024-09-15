using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Threading.Tasks;

namespace room_reservation.Controllers
{
    [Area("Admin")]
    public class PermissionController : Controller
    {
        private readonly PermissionDomain _PermissionDomain;
        private readonly RoleDomain _RoleDomain;
        private readonly BuildingDomain _BuildingDomain;
        private readonly UserDomain _UserDomain;

        public PermissionController(PermissionDomain permissionDomain, RoleDomain roleDomain, BuildingDomain buildingDomain, UserDomain userDomain)
        {
            _PermissionDomain = permissionDomain;
            _RoleDomain = roleDomain;
            _BuildingDomain = buildingDomain;
            _UserDomain = userDomain;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _PermissionDomain.getAllPermissions());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddPermission()
        {
            ViewBag.Building = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");
            ViewBag.Roles = new SelectList(await _RoleDomain.GetAllRoles(), "Id", "RoleName");
            return View();    

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPermission(PermissionViewModel permissionViewModel, int BuildingId)
        {
            ViewBag.Building = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");
            ViewBag.Roles = new SelectList(await _RoleDomain.GetAllRoles(), "Id", "RoleName");
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _UserDomain.GetUserByEmail(permissionViewModel.Email) == null)
                    {
                        return Json(new { success = false, message = "المستخدم غير موجود" });
                    }
                    if (await _PermissionDomain.permissionExists(permissionViewModel.Email))
                    {
                        return Json(new { success = false, message = "المستخدم يملك صلاحية" });
                    }

                    if (permissionViewModel.BuildingId == 0)
                    {
                        permissionViewModel.BuildingId = null;
                    }

                   
                    int check = await _PermissionDomain.AddPermission(permissionViewModel);

                    
                    if (check == 1)
                    {
                        var permissionLog = new PermissionsLog();
                        permissionLog.PermissionId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                        permissionLog.OperationType = "إضافة صلاحية";
                        permissionLog.DateTime = DateTime.Now;
                        permissionLog.PermissionType = User.FindFirst(ClaimTypes.Role).Value;
                        permissionLog.GrantedBy = User.FindFirst(ClaimTypes.Email).Value;
                        permissionLog.GrantedTo = permissionViewModel.Email;
                        permissionLog.AdditionalDetails = permissionViewModel.AdditionalDetails;
                        _PermissionDomain.AddPermissionLog(permissionLog);
                        return Json(new { success = true, message = "أُضِيفت الصلاحية بنجاح" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "لم تُضَاف الصلاحية" });
                    }
                }
                else
                {
                    var errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                    return Json(new { success = false, errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DisplayUserName(string email)
        {
            var user = await _UserDomain.GetUserByEmail(email);
            if (user != null)
            {
                return Json(new { success = true, userName = user.FullNameAR, phoneNumber = user.PhoneNumber });
            }
            else
            {
                return Json(new { success = false, message = "المتسخدم غير موجود" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditPermission(Guid guid)
        {
            ViewBag.Roles = new SelectList(await _RoleDomain.GetAllRoles(), "Id", "RoleName");
            ViewBag.Building = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");
            var permission = await _PermissionDomain.GetPermissionByGuid(guid);
            if (permission == null)
            {
                return NotFound();
            }

            var permissionViewModel = new PermissionViewModel
            {
                Id = permission.Id,
                Email = permission.Email,
                RoleId = permission.RoleId,
                BuildingId = permission.BuildingId,
                Guid = guid,

            };

            return View(permissionViewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditPermission(PermissionViewModel permissionViewModel)
        {
            ViewBag.Roles = new SelectList(await _RoleDomain.GetAllRoles(), "Id", "RoleName");
            ViewBag.Building = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _PermissionDomain.UpdatePermission(permissionViewModel);
                    if (result)
                    {
                        var permissionLog = new PermissionsLog();
                        permissionLog.PermissionId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                        permissionLog.OperationType = "تعديل صلاحية";
                        permissionLog.DateTime = DateTime.Now;
                        permissionLog.PermissionType = User.FindFirst(ClaimTypes.Role).Value; ;
                        permissionLog.GrantedBy = User.FindFirst(ClaimTypes.Email).Value; ;
                        permissionLog.GrantedTo = permissionViewModel.Email;
                        permissionLog.AdditionalDetails = permissionViewModel.AdditionalDetails;
                        _PermissionDomain.AddPermissionLog(permissionLog);
                        return Json(new { success = true, message = "عُدِّلت الصلاحية بنجاح" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "لم تُعدَّل الصلاحية" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
  

            return Json(new { success = false, message = "بيانات غير صالحة" });
        }

 

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeletePermission(Guid guid)
        {
            var check = await _PermissionDomain.DeletePermission(guid);
            if (check != null)
            {
                var permissionLog = new PermissionsLog();
                permissionLog.PermissionId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                permissionLog.OperationType = "حذف صلاحية";
                permissionLog.DateTime = DateTime.Now;
                permissionLog.PermissionType = User.FindFirst(ClaimTypes.Role).Value; ;
                permissionLog.GrantedBy = User.FindFirst(ClaimTypes.Email).Value; ;
                permissionLog.GrantedTo = check.Email;
                _PermissionDomain.AddPermissionLog(permissionLog);
                return Json(new { success = true, message = "حُذِفت الصلاحية" });
            }
            else
            {
                return Json(new { success = false, message = "لم تُحذَف الصلاحية" });
            }
        }



    }

}
