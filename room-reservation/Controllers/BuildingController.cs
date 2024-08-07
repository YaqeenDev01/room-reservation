using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Controllers
{
    public class BuildingController : Controller
    {

        private readonly BuildingDomain _BuildingDomain;
        public BuildingController(BuildingDomain buildingDomain)
        {
            _BuildingDomain = buildingDomain;

        }
        public async Task< IActionResult> Index( string successful,string Failed)
        {
            ViewData["successful"] = successful;
            ViewData["Failed"] = Failed;
            var buildings = await _BuildingDomain.GetAllBuilding(); 
            return View(buildings);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(BuildingViewModel building)
        {
            if (ModelState.IsValid)
            {
                int cheek = _BuildingDomain.InsertBuilding(building);
                if (cheek == 1)
                {
                    ViewData["successful"] = "تمت الإضافة بنجاح";
                    return View(building);
                }
            }
            else
                ViewData["Failed"] = "حدث خطأ أثناء معالجة طلبك، الرجاء المحاولة في وقت لاحق.";

           return View(building);

        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return View(_BuildingDomain.getBuildingByid(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( BuildingViewModel building)
        {
                if (ModelState.IsValid)
                {
                    int cheek = _BuildingDomain.updatBuilding(building);
                    if (cheek == 1)
                    {
                        ViewData["successful"] = "تم التعديل بنجاح";
                        return View(building);
                    }
                }
                else
                {
                    ViewData["Failed"] = "حدث خطأ أثناء معالجة طلبك، الرجاء المحاولة في وقت لاحق.";
                }

            return View(building);
        }

        public IActionResult Delet(Guid id)
        {
            string successful = "";
            string Failed = "";

            int cheek = _BuildingDomain.DeleteBuilding(id);
            if (cheek == 1)
                successful = "تم الحذف بنجاح";
                       
            else
                 Failed = "حدث خطأ أثناء معالجة طلبك، الرجاء المحاولة في وقت لاحق.";

            return RedirectToAction("Index", new { successful = successful , Failed = Failed });
        }
    }

    //public IActionResult Delet(BuildingViewModel building)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _BuildingDomain.DeleteBuilding(building);
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View();

    //}


}