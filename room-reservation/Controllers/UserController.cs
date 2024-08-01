
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
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
        public IActionResult Create()
        {
           // ViewData["UserId"] = new SelectList(_UserDomain.getAllUsers(), "Id", "FullNameAR");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( UserViewModel user){
            //is model state valid to check if the user has entered data or not and if not it will send that the [Required (ErrorMessage="")] in view model
            if (ModelState.IsValid)
            {
                _UserDomain.InsertUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View();


        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_UserDomain.getUserById(id));
        }

        [HttpPost] 
        public IActionResult Edit(tblUsers user)
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
    }  
} 
