using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Controllers
{
    public class PermissionController : Controller
    {
        private readonly PermissionDomain _PermissionDomain;
        private readonly RoleDomain _RoleDomain;

        public PermissionController(PermissionDomain permissionDomain, RoleDomain roleDomain)
        {
            _PermissionDomain = permissionDomain;
            _RoleDomain = roleDomain;
        }
        public async Task <IActionResult> Index()
        {
            return View(await _PermissionDomain.getAllPermissions());
        }
        [HttpGet]
        public async Task <IActionResult> AddPermission()
        {
            var roles = await _RoleDomain.GetAllRoles(); //await is added because a select statement is executed
            var permissionViewModel = new PermissionViewModel
            {
                Roles = roles.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.RoleName
 
                })
            };

            return View(permissionViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> addPermission(PermissionViewModel permissionViewModel)
        {
           if (ModelState.IsValid)
            {
                await _PermissionDomain.AddPermission(permissionViewModel); //await is added because an insert statement is executed.
                return RedirectToAction("Index");
            } 
           
            return View(permissionViewModel);
            
        }
        /*public IActionResult addPermission()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addPermission(tblPermissions permission)
        {
            _PermissionDomain.AddPermission(permission);
            return RedirectToAction("Index");
        }*/
    }
}
