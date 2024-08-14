
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
        public UserController(UserDomain UserDomain) {
            _UserDomain = UserDomain;

        }
        public IActionResult Index()
        {

            return View(_UserDomain.getAllUsers());
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
                    string userEamil = User.FindFirst(ClaimTypes.Email).Value;
                    return RedirectToAction("Index", "Home");
                }
                
            }
            catch
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
                    ViewData["Login_error"] = "البيانات غير موجودة";
                    return View();
                }
                else
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Email, userInfo.Email),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                    new Claim(ClaimTypes.Role, userInfo.UserType),
                    new Claim(ClaimTypes.GivenName, userInfo.FullNameAR)

                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                ViewData["Login_error"] = "خطأ: اسم المستخدم أو كلمة المرور غير صحيحة";
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(UserController.Login), "Login");
        }


    }
} 
