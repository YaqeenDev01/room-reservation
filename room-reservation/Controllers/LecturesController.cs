using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;


namespace room_reservation.Controllers
{
    public class LecturesController : Controller
    {
        private readonly lecturesDomain _lecturesDomain;

        public LecturesController(lecturesDomain lecturesDomain)
        {
            _lecturesDomain = lecturesDomain;
        }


        // GET: /Lecture/
        public async Task<IActionResult> Index()
        {
            var Lectures = await _lecturesDomain.getAlllectures();
            return View(Lectures);
        }

        // GET: /Lecture/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Lecture/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(LecturesViewModel lectures)
        {

            if (ModelState.IsValid)
            {
                int check = _lecturesDomain.Addlectures(lectures);
                if (check == 1)

                    ViewData["Successful"] = "تمت الإضافة بنجاح";




                else

                    ViewData["Falied"] = check;



            }



            return View();
        }
        [HttpGet]

        public IActionResult Edit(int Id)
        {
            return View(_lecturesDomain.getlecturesById(Id));


        }

        [HttpPost]
        public IActionResult Edit(LecturesViewModel lectures)
        {

            if (ModelState.IsValid)
            {
                int check = _lecturesDomain.EditLectures(lectures);
                if (check == 1)

                    ViewData["Successful"] = "تم التعديل بنجاح بنجاح";




                else

                    ViewData["Falied"] = check;



            }



            return View(lectures);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_lecturesDomain.getlecturesById(id));

        }

        [HttpPost]
        public IActionResult Delete(LecturesViewModel lectures)
        {

            if (ModelState.IsValid)
            {
                int check = _lecturesDomain.DeleteLectures(lectures);
                if (check == 1)

                    ViewData["Successful"] = "تم الحذف بنجاح";




                else

                    ViewData["Falied"] = check;



            }



            return View(lectures);
        }


    }
}
