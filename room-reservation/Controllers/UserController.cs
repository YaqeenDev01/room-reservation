
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
        public async Task <IActionResult> Index(String searchString)
        {
          
          
            var users = await _UserDomain.GetAllUsers();
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users
                    .Where(u => u.FullNameAR.Contains(searchString)||u.FullNameEN.Contains(searchString)||u.Email.Contains(searchString))
                    .ToList();
            }
            
            return View(users);
        }


        [HttpGet]
        public IActionResult AddUser()
        {
            ViewBag.UserType = new SelectList(new List<string>
            {
         
                "منسق الكلية",
                "مدير النظام"
              
            });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserViewModel user) {
            //is model state valid to check if the user has entered data or not and if not it will send that the [Required (ErrorMessage="")] in view model
            ViewBag.UserType = new SelectList(new List<string>
            {
               
                "منسق الكلية",
                "مدير النظام"
              
            });
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
        public async Task<IActionResult> EditUser(int id)
        {
            ViewBag.UserType = new SelectList(new List<string>
            {
               
                "منسق الكلية",
                "مدير النظام"
              
            });
            var user =_UserDomain.getUserById(id);
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
               
                "منسق الكلية",
                "مدير النظام"
              
            });
            try
            {
                if (ModelState.IsValid)
                {
                    int check =await _UserDomain.EditUser(user);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "ع\u064fد\u0651\u0650ل\u064eت البيانات بنجاح" });
                    } else
                    {
                        return Json(new { success = false, message = "لم ت\u064fعد\u0651\u064eل المعلومات" });
                    }

                }
                else
                {
                    return Json(new { success = false, message = "Model state is invalid" });
                }
            }
            catch (Exception exception)
            {
                return Json(new { success = false, message = exception.Message });
            }
        }
        // [HttpGet]
        // public IActionResult Delete(int id)
        // {
        //
        //     return View(_UserDomain.getUserById(id));
        // }
        // [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           await _UserDomain.DeleteUser(id);
           return Json(new { success = true });
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
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.GivenName, user.FullNameAR),
                    new Claim(ClaimTypes.Gender,user.GenderAR),
                 
                    

                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal);

                    if (role == "Admin" || role == "SiteAdmin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home",new {area=" "});
                    }
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
