using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using System.Security.Cryptography.X509Certificates;

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

        [HttpPost]
        public IActionResult AddUser(tblUsers user)
        {
            _UserDomain.AddUser(user);
            return RedirectToAction(nameof(Index));
        }


    }
} 

