using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using room_reservation.Domain;
using room_reservation.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace room_reservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SiteAdmin")]
    public class LecturesController : Controller
    {
        private readonly LecturesDomain _lecturesDomain;

        public LecturesController(LecturesDomain lecturesDomain)
        {
            _lecturesDomain = lecturesDomain;
        }

        // GET: /Lecture/
        public async Task<IActionResult> Index()
        {
            var lectures = await _lecturesDomain.GetAllLectures();
            return View(lectures);
        }

        // GET: /Lecture/Add
        [HttpGet]
        public IActionResult AddLecture()
        {
            return View();
        }

        // POST: /Lecture/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLecture(LecturesViewModel lectures)
        {
            if (ModelState.IsValid)
            {
                bool exists = _lecturesDomain.IsLectureExists(
                    //lectures.BuildingNo,
                    
                    lectures.RoomNo,
                    lectures.BuildingNameAR,
                    lectures.LectureDate,
                    lectures.StartLectureTime,
                    lectures.EndLectureTime,
                    lectures.Semester
                    );

                if (exists)
                {
                    return Json(new { success = false, message = "وقت المحاضرة محجوز مسبقاً" });
                }

                bool isOverlapping = await _lecturesDomain.IsLectureOverlapping(
                    //lectures.BuildingNo,
                    lectures.RoomNo,
                    lectures.BuildingNameAR,
                    lectures.LectureDate,
                    lectures.StartLectureTime,
                    lectures.EndLectureTime,
                    lectures.Semester
                );

                if (isOverlapping)
                {
                    return Json(new { success = false, message = "وقت المحاضرة يتعارض مع محاضرة أخرى " });
                }

                try
                {
                    var result = await _lecturesDomain.AddLecture(lectures);
                    if (result == 1)
                    {
                        return Json(new { success = true, message = "أُضيفت المحاضرة بنجاح" });
                    }
                    else
                    {
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

        // GET: /Lecture/Edit
        [HttpGet]
        public async Task<IActionResult> EditLecture(int id)
        {
            var lecture = await _lecturesDomain.GetLectureById(id);
            if (lecture == null)
            {
                return NotFound();
            }
            return View(lecture);
        }

        // POST: /Lecture/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLecture(LecturesViewModel lectures)
        {
            if (ModelState.IsValid)
            {
                bool exists = _lecturesDomain.IsLectureExists(
                    //lectures.BuildingNo,
                    lectures.RoomNo,
                    lectures.BuildingNameAR,
                    lectures.LectureDate,
                    lectures.StartLectureTime,
                    lectures.EndLectureTime,
                    lectures.Semester
                    );

                if (exists)
                {
                    return Json(new { success = false, message = "وقت المحاضرة محجوز مسبقاً" });
                }

                bool isOverlapping = await _lecturesDomain.IsLectureOverlapping(
                    //lectures.BuildingNo,
                    lectures.RoomNo,
                    lectures.BuildingNameAR,
                    lectures.LectureDate,
                    lectures.StartLectureTime,
                    lectures.EndLectureTime,
                    lectures.Semester
                );

                if (isOverlapping)
                {
                    return Json(new { success = false, message = "وقت المحاضرة يتعارض مع محاضرة أخرى " });
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
        
        // POST: /Lecture/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            await _lecturesDomain.DeleteLecture(id);
            return Json(new { success = true });
        }
    }
}

