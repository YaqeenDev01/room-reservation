using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
namespace room_reservation.Controllers
{
    public class FloorController : Controller
    {
        private readonly FloorDomain _FloorDomain;
        private readonly BuildingDomain _BuildingDomain;


        public FloorController(FloorDomain FloorDomain, BuildingDomain BuildingDomain)
        {
           
            _FloorDomain = FloorDomain;
            _BuildingDomain = BuildingDomain;


        }
        public async Task<IActionResult> Index()
        {
            var floors = await _FloorDomain.GetAllFloors(); 
            return View(floors);
        }
       

        [HttpGet]
        public async Task<IActionResult> AddFloor()
        {
     
            // Create a SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList( await _BuildingDomain.GetAllBuilding() ,"Id","BuildingNameAr");

            // Create a SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"Id","BuildingNo");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFloor(FloorViewModel floor)
        {
            // Create a SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding() ,"Id","BuildingNameAr");

            // Create a SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"Id","BuildingNo");
            try
            {
                if (ModelState.IsValid)
                {
                    await _FloorDomain.addFloor(floor);
                    return Json(new { success = true, message = "Added successfully" });
                   
                    
                }
                else
                {
                    return Json(new { success = false, message = "Invalid data" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
          
           // return View(floor);

        }
        [HttpGet]
        public async Task<IActionResult> EditFloor(Guid id)
        {
            //SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding() ,"Id","BuildingNameAr");
            //  SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"Id","BuildingNo");
            return View(_FloorDomain.GetFloorByGuid(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> EditFloor(FloorViewModel floor)
        {
            //SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding() ,"Id","BuildingNameAr");
            
            //  SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"Id","BuildingNo");
            try
            {
                if (ModelState.IsValid)
                {
                    await _FloorDomain.editFloor(floor);
                    return Json(new { success = true, message = "Added successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid data" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return View(floor);
        }

        public IActionResult Delete(Guid id)
        {
            string check = _FloorDomain.DeleteFloor(id);
                if (check == "1")
                
                    ViewData["Successful"] = "تم الحذف بنجاح";
                
                else
                    ViewData["Failed"] = check;
                
                _FloorDomain.DeleteFloor(id);
                return View();


        }
        //public async Task< IActionResult> Index()
        //{
        //    return View(await _FloorDomain.getAllFloors());
        //}
    }
}