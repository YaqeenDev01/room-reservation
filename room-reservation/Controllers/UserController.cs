using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using room_reservation.Domain;
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

    }  
} 

