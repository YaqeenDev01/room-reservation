using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Linq.Expressions;


namespace room_reservation.Areas.Admin.Controllers
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
        [Authorize(Roles = "Site Admin")]
        [HttpGet]
        public async Task<IActionResult> AddLecture()
        {
            return View();
        }
        [Authorize(Roles = "Site Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLecture(LecturesViewModel lectures)
        {
            if (ModelState.IsValid)
            {
                bool exists = _lecturesDomain.IsLectureExists(lectures.BuildingNo, lectures.RoomNo, lectures.LectureDate, lectures.StartLectureTime, lectures.EndLectureTime);

                if (exists)
                {
                    return Json(new { success = false, message = "وقت المحاضرة محجوز مسبقا" });
                }

                try
                {
                    var check = await _lecturesDomain.Addlecture(lectures);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "أُضيفت المحاضرة بنجاح" });
                    }
                    else {
                        return Json(new { success = false, message = "لم تضاف المحاضرة" });

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                var errors = ModelState.ToDictionary(
                      kvp => kvp.Key,
                      kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                  );

                return Json(new { success = false, errors });
            }
        }

        [Authorize(Roles = "Site Admin")]
        [HttpGet]

        public async Task<IActionResult> EditLecture(int Id)
        {
            return View(_lecturesDomain.getlecturesById(Id));
        }
        [Authorize(Roles = "Site Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLecture(LecturesViewModel lectures)
        {
            if (ModelState.IsValid)
            {
                bool exists = _lecturesDomain.IsLectureExists(lectures.BuildingNo, lectures.RoomNo, lectures.LectureDate, lectures.StartLectureTime, lectures.EndLectureTime);

                if (exists)
                {
                    return Json(new { success = false, message = "وقت المحاضرة محجوز مسبقاً" });
                }

                try
                {
                    var result = await _lecturesDomain.EditLecture(lectures);
                    if (result == 1)
                    {
                        return Json(new { success = true, message = "عُدلت المحاضرة" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "لم يتم التعديل" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            return Json(new { success = false, message = "فشلت العملية" });
        }
        [Authorize(Roles = "Site Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteLecture(int id)
        {

            await _lecturesDomain.DeleteLecture(id);
            return Json(new { success = true });
        }
    }
}
