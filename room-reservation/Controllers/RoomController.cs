using Microsoft.AspNetCore.Mvc;

namespace room_reservation.Controllers
{
    public class _RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
