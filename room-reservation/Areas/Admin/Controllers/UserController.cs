using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.ViewModel;

namespace room_reservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserDomain _UserDomain;
        private readonly PermissionDomain _PermissionDomain;
        private readonly BuildingDomain _BuildingnDomain;
        public UserController(UserDomain UserDomain, PermissionDomain PermissionDomain, BuildingDomain BuildingDomain)
        {
            _UserDomain = UserDomain;
            _PermissionDomain = PermissionDomain;
            _BuildingnDomain = BuildingDomain;
            

        }
        public async Task<IActionResult> Index(String searchString)
        {


            var users = await _UserDomain.GetAllUsers();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            ViewBag.Building = new SelectList(await _BuildingnDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");
            ViewBag.UserType = new SelectList(new List<string>
            {

                "موظف",
                "طالب",
                "عضو هيئة التدريس"

            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel user)
        {
            ViewBag.Building = new SelectList(await _BuildingnDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");

            ViewBag.UserType = new SelectList(new List<string>
            {

                "موظف",
                "طالب",
                "عضو هيئة التدريس"

            });
            try
            {
                if (ModelState.IsValid)
                {
                    var check = await _UserDomain.AddUser(user);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "أُضَاف المستخدم بنجاح" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "لم يُضّاف المستخدم" });
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
                return Json(new { success = false, messgae = ex.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            ViewBag.UserType = new SelectList(new List<string>
            {

                "موظف",
                "طالب",
                "عضو هيئة التدريس"

            });
            var user = _UserDomain.getUserById(id);
            if (user == null)
            {
                return NotFound(); // Handle the case where the user doesn't exist
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel user)
        {
            ViewBag.UserType = new SelectList(new List<string>
            {

                "موظف",
                "طالب",
                "عضو هيئة التدريس"

            });
            try
            {
                if (ModelState.IsValid)
                {
                    int check = await _UserDomain.EditUser(user);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "عُدِّلت المعلومات بنجاح" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "لم تُعدَل المعلومات" });
                    }

                }
                else
                {
                    return Json(new { success = false, message = "بيانات غير صالحة" });
                }
            }
            catch (Exception exception)
            {
                return Json(new { success = false, message = exception.Message });
            }
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            await _UserDomain.DeleteUser(id);
            return Json(new { success = true });
        }
    }
}
