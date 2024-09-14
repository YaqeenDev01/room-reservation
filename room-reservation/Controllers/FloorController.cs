using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Security.Claims;
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
        //return all floors in the view
        public async Task<IActionResult> Index(String searchString)
        {
            var floors = await _FloorDomain.GetAllFloors();
            //activate quick search based on the building name 
            if (!String.IsNullOrEmpty(searchString))
                
            {
                floors = floors
                    .Where(f => f.BuildingNameAr.Contains(searchString))
                    .ToList();
            }

            return View(floors);
        }
       

        [HttpGet]
        public async Task<IActionResult> AddFloor()
        {
     
            // Create a SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList( await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNameAr");

            // Create a SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFloor(FloorViewModel floor, int BuildingId)
        {
            floor.Email =User.FindFirst(ClaimTypes.Email).Value;
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNo");
            try
            {
                if (ModelState.IsValid)
                {
                    int check = await _FloorDomain.addFloor(floor);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "أ\u064fض\u0650يف\u064e الطابق بنجاح" });

                    }
                    else if(check == 2)
                    {
                        return Json(new { success = false, message = "رقم الطابق م\u064fدرج مسبق\u064bا" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "لم يض\u064eاف الطابق" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "لابد من إضافة طابق" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditFloor(Guid id)
        {
            //SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNameAr");
            //  SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNo");
            return View(await _FloorDomain.GetFloorByGuid(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> EditFloor(FloorViewModel floor)
        {
            //SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNameAr");
            
            //  SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNo");
            try
            {
                if (ModelState.IsValid)
                {
                    int check = await _FloorDomain.editFloor(floor);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "ع\u064fد\u0651\u0650ل\u064eت البيانات بنجاح" });

                    }
                    else if(check == 2)
                    {
                        return Json(new { success = false, message = "رقم الطابق م\u064fدرج مسبق\u064bا" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "لم ت\u064fعد\u0651\u064eل المعلومات" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "لابد من إدخال بيانات الطابق" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

          //  return View(floor);
        }
        public async Task<IList<FloorViewModel>> getFloorbyGuid(Guid id)
        {
            return await _FloorDomain.GetFloorByBuildingGuid(id);
            
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _FloorDomain.DeleteFloor(id);
            return Json(new { success = true });
        }
        
        
        //public async Task< IActionResult> Index()
        //{
        //    return View(await _FloorDomain.getAllFloors());
        //}
    }
}