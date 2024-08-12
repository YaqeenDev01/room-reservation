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



        public FloorController(FloorDomain FloorDomain)
        {
           
            _FloorDomain = FloorDomain;
  

        }
        public async Task<IActionResult> Index()
        {
            var floors = await _FloorDomain.GetAllFloors(); 
            return View(floors);
        }
       

        [HttpGet]
        public IActionResult Create()
        {
     
            // Create a SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList( _FloorDomain.GetAllBuilding() ,"Id","BuildingNameAr");

            // Create a SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(_FloorDomain.GetAllBuilding() ,"Id","BuildingNo");
            return View();
        }

        [HttpPost]
        public IActionResult Create(FloorViewModel floor)
        {
            // Create a SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList( _FloorDomain.GetAllBuilding() ,"Id","BuildingNameAr");

            // Create a SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(_FloorDomain.GetAllBuilding() ,"Id","BuildingNo");
            
            if (ModelState.IsValid)
            {
                string check = _FloorDomain.addFloor(floor);
                if (check == "1")
                {
                    ViewData["Successful"] = "تمت العملية بنجاح";
                }
                else
                //ask shatha 
                    ViewData["Failed"] = check;
            }

            return View(floor);

        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList( _FloorDomain.GetAllBuilding() ,"Id","BuildingNameAr");
            //  SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(_FloorDomain.GetAllBuilding() ,"Id","BuildingNo");
            return View(_FloorDomain.GetFloorByGuid(id));
        }
        
        [HttpPost]
        public IActionResult Edit(FloorViewModel floor)
        {
            //  SelectList buildingsName
            ViewBag.buildingsName = new SelectList( _FloorDomain.GetAllBuilding() ,"Id","BuildingNameAr");

            //  SelectList  buildingsName
            ViewBag.buildingsNo = new SelectList(_FloorDomain.GetAllBuilding() ,"Id","BuildingNo");
            if (ModelState.IsValid)
            {
                    string check = _FloorDomain.editFloor(floor);
                    if (check == "1")
                    {
                        ViewData["Successful"] = "تمت العملية بنجاح";
                    }
                    else
                        ViewData["Failed"] = check;
                
              _FloorDomain.editFloor(floor);
               
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