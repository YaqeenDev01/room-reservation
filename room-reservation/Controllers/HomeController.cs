using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using room_reservation.Models;
using System.Diagnostics;

namespace room_reservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Orders() {

            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Add_order()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}