using Microsoft.AspNetCore.Mvc;
using room_reservation.Domain;
using room_reservation.ViewModel;

namespace room_reservation.Controllers
{
    
    public class BookingController : Controller
    {

        private readonly BookingDomain _BookingDomain;
        public BookingController(BookingDomain bookingDomain)
        {
            _BookingDomain = bookingDomain;

        }
        public async Task<IActionResult> Index()
        {
            var booking = await _BookingDomain.GetAllBooking();
            return View(booking);
        }

        [HttpGet]
        public IActionResult add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult add(BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                _BookingDomain.AddBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                _BookingDomain.DeleteBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        [HttpGet]
        public IActionResult Details(Guid id) {
            return View(_BookingDomain.getBookinggByid(id));
        }

        [HttpPost]
        public IActionResult Details(BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                
                _BookingDomain.DetailsBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

    }

}

