using Microsoft.AspNetCore.Mvc;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
namespace room_reservation.Controllers
{
    public class FloorController : Controller
    {
        private readonly FloorDomain _FloorDomain;
        public FloorController(FloorDomain FloorDomain)
        {
            _FloorDomain = FloorDomain;

        }

        //public async Task< IActionResult> Index()
        //{
        //    return View(await _FloorDomain.getAllFloors());
        //}
    }
}