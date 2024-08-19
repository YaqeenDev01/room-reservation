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

        // POST: /Lecture/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLecture(LecturesViewModel lectures)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _lecturesDomain.Addlecture(lectures);
                    return Json(new { success = true, message = "Added successfully" });
                }
                else
                {
                    return Json(new { success = true, message = "Invalid Data" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]

        public async Task<IActionResult> EditLecture(int Id)
        {
            return View(_lecturesDomain.getlecturesById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> EditLecture(LecturesViewModel lectures)
        {
            if (ModelState.IsValid)
                try
                {
                    var result = await _lecturesDomain.EditLecture(lectures);
                    if (result == 1)
                    {

                        return Json(new { success = true, message = "Updated successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Update Failed" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            return Json(new { success = false, message = "Invalid Data" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            
            await _lecturesDomain.DeleteLecture(id);
            return Json(new { success = true });
        }
        //public IActionResult DeleteLecture(LecturesViewModel lectureinfo)
        //{
        //    string successful = "";
        //    string Failed = "";

        //    int cheek =
        //    _lecturesDomain.DeleteLecture(Id);
        //    if (cheek == 1)
        //        successful = "تم الحذف بنجاح";

        //    else
        //        Failed = "حدث خطأ أثناء معالجة طلبك، الرجاء المحاولة في وقت لاحق.";

        //    return RedirectToAction("Index", new { successful = successful, Failed = Failed });
    }



        //[HttpPost]
        //public IActionResult DeleteLecture(int id)
        //{
        //    _lecturesDomain.DeleteLecture(id);
        //    return Json(new { success = true });
        //}

        //}


        //[HttpGet]
        //     public async Task<IActionResult> DeleteLecture(int id)
        //{
        //    await _lecturesDomain.DeleteLecture(id);
        //    return Json(new { success = true });
        //}
        //{
        //    return View(_lecturesDomain.getlecturesById(id));

        //}

        //[HttpPost]
        //public async Task<IActionResult> DeleteLecture(LecturesViewModel lectures)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            int check = _lecturesDomain.DeleteLecture(lectures);
        //            if (check == 1)

        //                ViewData["Successful"] = "تم الحذف بنجاح";




        //            else

        //                ViewData["Falied"] = check;



        //        }



        //        return View(lectures);
        //    }
        //    catch { 


        //    }} }
    }

