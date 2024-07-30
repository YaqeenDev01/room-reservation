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
        public async Task< IActionResult> Index()
        {
            var buildings = await _BuildingDomain.GetAllBuilding(); 
            return View(buildings);
        }

        [HttpGet]
        public IActionResult add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult add(BuildingViewModel building)
        {
            if (ModelState.IsValid)
            {
               _BuildingDomain.InsertBuilding(building);
                return RedirectToAction(nameof(Index));
            }
            return View();
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
                    _BuildingDomain.updatBuilding(building);
                    return RedirectToAction(nameof(Index));
                }    
                return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delet(BuildingViewModel building)
        {
            if (ModelState.IsValid)
            {
                _BuildingDomain.DeletBulding(building);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
    }
}
