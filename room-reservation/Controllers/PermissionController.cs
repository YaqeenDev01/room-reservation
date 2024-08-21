﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace room_reservation.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            return View(await _PermissionDomain.getAllPermissions());
        }

        [HttpGet]
        public async Task<IActionResult> AddPermission()
        {
            ViewBag.Building = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");
            ViewBag.Roles = new SelectList(await _RoleDomain.GetAllRoles(), "Id", "RoleName");
            return View();    

        }

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
                        return Json(new { success = false, message = "User not found" });
                    }
                    if (permissionViewModel.BuildingId == 0)
                    {
                        permissionViewModel.BuildingId = null;
                    }

                    int check = await _PermissionDomain.AddPermission(permissionViewModel);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "Added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Adding permission failed" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Model state is invalid" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DisaplyUserName(string email)
        {
            var user = await _UserDomain.GetUserByEmail(email);
            if (user != null)
            {
                return Json(new { success = true, userName = user.FullNameAR });
            }
            else
            {
                return Json(new { success = false, message = "User not found" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditPermission(PermissionViewModel permissionViewModel)
        {
            if(ModelState.IsValid)
    {
                try
                {
                    

                    var result = await _PermissionDomain.UpdatePermission(permissionViewModel);
                    if (result)
                    {
                        return Json(new { success = true, message = "Updated successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Update failed" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpGet]

        public async Task<IActionResult> EditPermission(int Id)
        {


            var permission = await _PermissionDomain.GetPermissionById(Id);
            var roles = await _RoleDomain.GetAllRoles();
            var buildings = await _BuildingDomain.GetAllBuilding();

            var permissionViewModel = new PermissionViewModel
            {
                Id = permission.Id,
                Email = permission.Email,
                RoleId = permission.RoleId,
                BuildingId = permission.BuildingId,

                Roles = roles.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.RoleName
                }).ToList(),
                Buildings = buildings.Select(x => new SelectListItem
                {
                    Value = x.BuildingId.ToString(),
                    Text = x.BuildingNameAr
                }).ToList()
            };

            return View(permissionViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> DeletePermission(int id)
        {
            await _PermissionDomain.DeletePermission(id);
            return Json(new { success = true });
        }



    }

}
