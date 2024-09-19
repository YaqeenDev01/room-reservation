using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using room_reservation.Domain;
using room_reservation.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using OfficeOpenXml;

namespace room_reservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, SiteAdmin")]
    public class LecturesController : Controller
    {
        private readonly LecturesDomain _lecturesDomain;

        public LecturesController(LecturesDomain lecturesDomain)
        {
            _lecturesDomain = lecturesDomain;
        }

        // GET: /Lecture/
        [Authorize(Roles = "Admin, SiteAdmin")]
        public async Task<IActionResult> Index()
        {
            var lectures = await _lecturesDomain.GetAllLectures();
            return View(lectures);
        }

        // GET: /Lecture/Add
        [Authorize(Roles = "Admin, SiteAdmin")]
        [HttpGet]
        public IActionResult AddLecture()
        {
            return View();
        }

        // POST: /Lecture/Add
        [Authorize(Roles = "Admin, SiteAdmin")]
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
        [Authorize(Roles = "Admin, SiteAdmin")]
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
        [Authorize(Roles = "Admin, SiteAdmin")]
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
        [Authorize(Roles = "Admin, SiteAdmin")]
        [HttpPost]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            await _lecturesDomain.DeleteLecture(id);
            return Json(new { success = true });
        }
        public async Task<ActionResult> ExportLecture()
        {
            var dataLecture = await _lecturesDomain.GetExportableLectures();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("المحاضرات");

                worksheet.Cells[1, 1].Value = "اسم المبنى";
                worksheet.Cells[1, 2].Value = "الفصل الدراسي";
                worksheet.Cells[1, 3].Value = "رقم القاعة ";
                worksheet.Cells[1, 4].Value = "تاريخ المحاضرة";
                worksheet.Cells[1, 5].Value = "وقت بدء المحاضرة";
                worksheet.Cells[1, 6].Value = "وقت انتهاء المحاضرة ";

                // Add data rows
                int row = 2;
                foreach (var lectures in dataLecture)
                {
                    worksheet.Cells[row, 1].Value = lectures.BuildingNameAR;
                    worksheet.Cells[row, 2].Value = lectures.Semester;
                    worksheet.Cells[row, 3].Value = lectures.RoomNo;
                    worksheet.Cells[row, 4].Value = lectures.LectureDate;
                    worksheet.Cells[row, 4].Style.Numberformat.Format = "yyyy-MM-dd";
                    worksheet.Cells[row, 5].Value = lectures.StartLectureTime;
                    worksheet.Cells[row, 5].Style.Numberformat.Format = "hh:mm";
                    worksheet.Cells[row, 6].Value = lectures.EndLectureTime;
                    worksheet.Cells[row, 6].Style.Numberformat.Format = "hh:mm";
                    row++;
                }

                // Set column widths to fit content
                worksheet.Column(1).Width = 15; // Adjust based on your content
                worksheet.Column(2).Width = 15; // Adjust based on your content
                worksheet.Column(3).Width = 10; // Adjust based on your content
                worksheet.Column(4).Width = 15; // Adjust based on your content
                worksheet.Column(5).Width = 10; // Adjust based on your content
                worksheet.Column(6).Width = 10; // Adjust based on your content

                // Generate the file
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fName = $"المحاضرات-{DateTime.Now:yyyyMMdd}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fName);
            }
        }

    }
}

