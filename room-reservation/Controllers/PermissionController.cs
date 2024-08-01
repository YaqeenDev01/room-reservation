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
        private readonly BuildingDomain _BuildingDomain;

        public PermissionController(PermissionDomain permissionDomain, RoleDomain roleDomain, BuildingDomain buildingDomain)
        {
            _PermissionDomain = permissionDomain;
            _RoleDomain = roleDomain;
            _BuildingDomain = buildingDomain;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _PermissionDomain.getAllPermissions());
        }
        //Adding a permission
        [HttpGet]
        public async Task<IActionResult> AddPermission()
        {
            var roles = await _RoleDomain.GetAllRoles(); //await is added because a select statement is executed
            var buildings = await _BuildingDomain.GetAllBuilding();
            var permissionViewModel = new PermissionViewModel
            {
                Roles = roles.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.RoleName

                }).ToList(),

                Buildings = buildings.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.BuildingNameAr,

                }).ToList()

            };

            // Repopulate dropdowns in case of invalid model state
            permissionViewModel.Roles = (await _RoleDomain.GetAllRoles())
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.RoleName
                }).ToList();

            permissionViewModel.Buildings = (await _BuildingDomain.GetAllBuilding())
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.BuildingNameAr
                }).ToList();




            return View(permissionViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddPermission(PermissionViewModel permissionViewModel)
        {
            if (ModelState.IsValid)
            {
                await _PermissionDomain.AddPermission(permissionViewModel); //await is added because an insert statement is executed.
                return RedirectToAction("Index");
            }

            return View(permissionViewModel);

        }

        //Updating a permission

        [HttpGet]
        public async Task<IActionResult> EditPermission(Guid guid)
        {
            var permission = _PermissionDomain.GetPermissionByGuid(guid);
            var roles = _RoleDomain.GetAllRoles();
            var buildings= _BuildingDomain.GetAllBuilding();

            var permissionViewModel = new PermissionViewModel
            {
                

            };

            return View();
        }

    }
}
