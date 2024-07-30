using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;

namespace room_reservation.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDomain _UserDomain;
        public UserController(UserDomain UserDomain)
        {
            _UserDomain = UserDomain;

        }
        public IActionResult Index()
        {

            return View(_UserDomain.getAllUsers());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_UserDomain.getAllUsers(), "Id", "RoleNameAr"); 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(tblUsers users)
        {
            _UserDomain.InsertUser(users);
            return RedirectToAction(nameof(Index));
        }


    }
}
    



