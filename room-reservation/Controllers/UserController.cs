
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace room_reservation.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDomain _UserDomain;
        private readonly PermissionDomain _PermissionDomain;
        public UserController(UserDomain UserDomain, PermissionDomain PermissionDomain) {
            _UserDomain = UserDomain;
            _PermissionDomain = PermissionDomain;

        }
        public async Task <IActionResult> Index()
        {
            var users = await _UserDomain.GetAllUsers();
            return View(users);
        }


        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserViewModel user) {
            //is model state valid to check if the user has entered data or not and if not it will send that the [Required (ErrorMessage="")] in view model
            try
            {
                if (ModelState.IsValid)
                {
                    await _UserDomain.AddUser(user);
                    return Json(new { success = true, message = "Added successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid data" });
                }

            }
            catch (Exception ex)
            {
                return Json(new {success = false, messgae =  ex.Message});
            }
           

        
        }
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            return View(_UserDomain.getUserById(id));
        }

        [HttpPost]
        public IActionResult EditUser(tblUsers user)
        {
            if (ModelState.IsValid)
            {
                _UserDomain.EditUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View();


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {

            return View(_UserDomain.getUserById(id));
        }
        [HttpPost]
        public IActionResult Delete(tblUsers user)
        {
            _UserDomain.DeleteUser(user);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [AllowAnonymous] //Allow any one to enter this page
        public async Task <IActionResult> Login()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string userName = User.FindFirst(ClaimTypes.Name).Value;
                    return RedirectToAction("Index", "Home");
                }
                
            }
            catch (Exception ex)
            {
                
                return View();
            }

            return View();

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(UserViewModel userInfo)
        {
            try
            {
                UserViewModel user = await _UserDomain.GetAccountsForLogin(userInfo);

                if ( user == null)
                {
                    ViewData["Login_error"] = "المستخدم غير موجود";
                    return View();
                }
                else
                {

                    string role = "";
                    PermissionViewModel permission = await _PermissionDomain.GetPermissionByEmail(user.Email);
                    
                    if (permission == null) {
                        role = "No Role";
                    }
                    else
                    {
                        role = permission.RoleName;
                    }
                    var identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name , user.FullNameEN),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.GivenName, user.FullNameAR)

                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewData["Login_error"] = "خطأ: اسم المستخدم أو كلمة المرور غير صحيحة";
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }


    }
} 
