using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Linq.Expressions;


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
        public async Task<IActionResult> AddLecture()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLecture(LecturesViewModel lectures)
        {
            if (ModelState.IsValid)
            {
                bool exists = _lecturesDomain.IsLectureExists(lectures.BuildingNo, lectures.RoomNo, lectures.LectureDate, lectures.StartLectureTime, lectures.EndLectureTime);

                if (exists)
                {
                    return Json(new { success = false, message = "The lecture time slot is already booked." });
                }

                try
                {
                    await _lecturesDomain.Addlecture(lectures);
                    return Json(new { success = true, message = "Lecture added successfully." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            return Json(new { success = false, message = "Invalid data." });
        }
        // POST: /Lecture/Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddLecture(LecturesViewModel lectures)
        //{
        //    try
        //    {

        //        if (ModelState.IsValid)
        //        {
        //            await _lecturesDomain.Addlecture(lectures);
        //            return Json(new { success = true, message = "Added successfully" });
        //        }
        //        else
        //        {
        //            return Json(new { success = true, message = "Invalid Data" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

        [HttpGet]

        public async Task<IActionResult> EditLecture(int Id)
        {
            return View(_lecturesDomain.getlecturesById(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLecture(LecturesViewModel lectures)
        {
            if (ModelState.IsValid)
            {
                bool exists = _lecturesDomain.IsLectureExists(lectures.BuildingNo, lectures.RoomNo, lectures.LectureDate, lectures.StartLectureTime, lectures.EndLectureTime);

                if (exists)
                {
                    return Json(new { success = false, message = "The lecture time slot is already booked." });
                }

                try
                {
                    var result = await _lecturesDomain.EditLecture(lectures);
                    if (result == 1)
                    {
                        return Json(new { success = true, message = "Lecture updated successfully." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Update failed." });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            return Json(new { success = false, message = "Invalid data." });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLecture(int id)
        {

            await _lecturesDomain.DeleteLecture(id);
            return Json(new { success = true });
        }
    }
}
        